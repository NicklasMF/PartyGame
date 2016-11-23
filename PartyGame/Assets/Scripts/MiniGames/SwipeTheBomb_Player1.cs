using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SwipeTheBomb_Player1 : NetworkBehaviour {

	// UI
	[SerializeField] Button startGameBtn;

	// Generelle
	bool isTheServer;
	GameObject ui;

	// Spilvariabler
	[SyncVar (hook = "OnChangeState")] public bool iHaveTheBomb;
	bool gameStarted;
	bool gameEnded;
	float timeToBlow;
	float countdownToStart;
	bool startCountdown;
	List<Player> playerArray = new List<Player>();
	int whoHasTheBombPlayerIndex;

	// Use this for initialization
	void Start () {
		isTheServer = transform.GetComponent<PlayerSetup>().isTheServer;	
		GeneratePlayerArray();

		if (!isLocalPlayer)
			return;

		iHaveTheBomb = false;

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
			ui.GetComponent<SwipeTheBomb_PlayerUI>().HasTheBomb(iHaveTheBomb);
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

		if (isLocalPlayer && Input.GetKeyDown(KeyCode.M)) CmdSetTheBomb(GetComponent<Player>().seatNo-1);

		//if (havingTheBomb) {
			if (SwipeManager.IsSwipingRight() || Input.GetKeyDown(KeyCode.Y)) {
				Debug.Log("Aflevér bomben til højre");
				SendingTheBomb(1);
			} else if (SwipeManager.IsSwipingLeft() || Input.GetKeyDown(KeyCode.T)) {
				Debug.Log("Aflevér bomben til venstre");
				SendingTheBomb(-1);
			}
		//}
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log(whoHasTheBombPlayerIndex);
			Debug.Log(playerArray[whoHasTheBombPlayerIndex]);
		}

	}

	void OnChangeState(bool _bool) {
		//iHaveTheBomb = _bool;
		Debug.Log(transform.name + " er ændret til " + _bool);
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
		int playerCount = playerArray.Count;
		int pickPlayer = Random.Range(1,playerCount);
		whoHasTheBombPlayerIndex = pickPlayer - 1;
		Debug.Log(playerArray[whoHasTheBombPlayerIndex] + " got the bomb. PlayerIndex: "+whoHasTheBombPlayerIndex);

		foreach(Player _player in GameManager.GetPlayers()) {
			for (int i = 1; i <= 4; i++) {
				Player _pl = WhoHasSeat(i);
				if (_pl != null) {
					if (_pl.seatNo == _player.seatNo) {
						if (_pl == playerArray[whoHasTheBombPlayerIndex]) {
							//_pl.GetComponent<SwipeTheBomb_Player>().RpcGotTheBomb();
						}
					}
				}
			}
		}
	}

	[ClientRpc]
	public void RpcGotTheBomb() {
		if (isLocalPlayer) {
			iHaveTheBomb = true;
			ui.GetComponent<SwipeTheBomb_PlayerUI>().HasTheBomb(iHaveTheBomb);
		}
	}

	[Client]
	public void SendingTheBomb(int _receiver) {
		if (isLocalPlayer) {
			iHaveTheBomb = false;
			ui.GetComponent<SwipeTheBomb_PlayerUI>().HasTheBomb(iHaveTheBomb);
			whoHasTheBombPlayerIndex += _receiver;
			if (whoHasTheBombPlayerIndex < 0) {
				whoHasTheBombPlayerIndex = playerArray.Count - 1;
			} else if (whoHasTheBombPlayerIndex > playerArray.Count - 1) {
				whoHasTheBombPlayerIndex = 0;
			}
			CmdSetTheBomb(whoHasTheBombPlayerIndex);
		}

	}

	[Command]
	public void CmdSetTheBomb(int _index) {
		whoHasTheBombPlayerIndex = _index;
		int _seatRecieve = _index + 1;
		foreach(Player _pl in playerArray) {
			if (_pl.seatNo == _seatRecieve) {
				//Debug.Log(_pl.name + " got the bomb.");
				//_pl.GetComponent<SwipeTheBomb_Player>().RpcGotTheBomb();
			}
		}
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
