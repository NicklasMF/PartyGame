using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SwipeTheBomb_Player : NetworkBehaviour {

	// UI
	[SerializeField] Button startGameBtn;

	// Generelle
	bool isTheServer;
	GameObject ui;

	// Spilvariabler
	[SyncVar] public int haveTheBombIndex;
	bool gameStarted;
	bool gameEnded;
	float timeToBlow;
	float countdownToStart;
	bool startCountdown;
	List<Player> playerArray = new List<Player>();

	// Use this for initialization
	void Start () {
		isTheServer = transform.GetComponent<PlayerSetup>().isTheServer;	
		GeneratePlayerArray();
		haveTheBombIndex = -1;
		if (!isLocalPlayer)
			return;

		if (isTheServer) {
			ui = GetComponent<GameController>().pregameUI;
			countdownToStart = 2f;
			startCountdown = false;
			timeToBlow = 20f;
			gameStarted = false;
			gameEnded = false;

			AddBtnListener();

		} else {
			ui = GetComponent<GameController>().playerUI;
			bool _bool = (haveTheBombIndex == GetComponent<Player>().seatIndex);
			ui.GetComponent<SwipeTheBomb_PlayerUI>().HasTheBomb(_bool);
		}
	}

	public void Update() {
		if (!isLocalPlayer) {
			return;
		}
		if (isTheServer) {
			if (gameStarted && !gameEnded) {
				if (timeToBlow > 0f) {
					timeToBlow -= Time.deltaTime;
				} else {
					CmdBlowBomb();
				}
			}

			if (startCountdown) {
				if (countdownToStart > 0f) {
					countdownToStart -= Time.deltaTime;
				} else {
					StartGame();
					startCountdown = false;
				}
			}
		}

		if (gameStarted) {
			//if (havingTheBomb) {
			if (SwipeManager.IsSwipingRight() || Input.GetKeyDown(KeyCode.Y)) {
				Debug.Log("Aflevér bomben til højre");
				SendingTheBomb(1);
			} else if (SwipeManager.IsSwipingLeft() || Input.GetKeyDown(KeyCode.T)) {
				Debug.Log("Aflevér bomben til venstre");
				SendingTheBomb(-1);
			}
			//}

		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log(haveTheBombIndex);
			Debug.Log(playerArray[haveTheBombIndex]);
		}

	}

	void AddBtnListener() {
		GameObject sceneManager = GameObject.FindGameObjectWithTag("ServerUI");
		startGameBtn = sceneManager.GetComponent<SwipeTheBomb_ServerUI>().startGameBtn;
		startGameBtn.enabled = true;
		startGameBtn.onClick.AddListener(BtnStartGame);
	}

	void GeneratePlayerArray() {
		foreach(Player _player in GameManager.GetPlayers()) {
			playerArray.Add(_player);
		}
	}

	void BtnStartGame() {
		if (isLocalPlayer && isTheServer) {
			startGameBtn.enabled = false;
			startCountdown = true;
			Debug.Log("Venter 2 sekunder");
		}
	}

	void StartGame() {
		Debug.Log("Spillet starter");
		gameStarted = true;
		CmdPlaceBomb();
	}

	[Command]
	public void CmdPlaceBomb() {
		Debug.Log("Cmd Called");
		int playerCount = playerArray.Count;
		int playerPicked = Random.Range(1,playerCount);
		haveTheBombIndex = playerPicked - 1;
		Debug.Log(playerArray[haveTheBombIndex].name + " has the bomb");
		RpcBombSended(playerArray[haveTheBombIndex].seatIndex);

	}

	[ClientRpc]
	public void RpcBombSended(int _index) {
		Debug.Log("RPC Called: "+_index);
		SetTheBomb(_index);
		if (isLocalPlayer) {
			if (!isTheServer) {
				bool _bool = (haveTheBombIndex == GetComponent<Player>().seatIndex);
				ui.GetComponent<SwipeTheBomb_PlayerUI>().HasTheBomb(_bool);
				ui.GetComponent<SwipeTheBomb_PlayerUI>().SetBomb(haveTheBombIndex.ToString());
			}
		}
	}

	[Client]
	public void SendingTheBomb(int _receiver) {
		if (isLocalPlayer) {
			haveTheBombIndex += _receiver;
			if (haveTheBombIndex < 0) {
				haveTheBombIndex = playerArray.Count;
			} else if (haveTheBombIndex > playerArray.Count - 1) {
				haveTheBombIndex = 0;
			}
			Debug.Log(haveTheBombIndex);
			bool _bool = (haveTheBombIndex == GetComponent<Player>().seatIndex);
			ui.GetComponent<SwipeTheBomb_PlayerUI>().HasTheBomb(_bool);
			ui.GetComponent<SwipeTheBomb_PlayerUI>().SetBomb(haveTheBombIndex.ToString());
			CmdSetTheBomb(haveTheBombIndex);
		}

	}

	[Command]
	public void CmdSetTheBomb(int _index) {
		haveTheBombIndex = _index;
		RpcBombSended(playerArray[haveTheBombIndex].seatNo);
	}

	void SetTheBomb(int _index) {
		haveTheBombIndex = _index;
	}

	Player WhoHasSeat(int _seatNo) {
		foreach(Player _player in GameManager.GetPlayers()) {
			if (_player.seatNo == _seatNo) {
				return _player;
			}
		}
		return null;
	}

	[Command]
	public void CmdBlowBomb() {
		gameEnded = true;
	}


}