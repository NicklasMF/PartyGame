using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;

public class Player : NetworkBehaviour {

	private bool _isDead = false;
	public bool isDead {
		get {return _isDead;}
		protected set {_isDead = value;}
	}

	[SyncVar] public string username = "Loading...";
	[SyncVar] public int score = 0;
	[SyncVar(hook = "UpdateSeatNo")] public int seatNo;

	public Color color;
	public int seatIndex;
	private int playerIndex;

	void Start() {
		DontDestroyOnLoad(gameObject);

		SetColor();

		if (isLocalPlayer) {
			SetPlayerIndex();
			seatNo = -1;
		}
	}

	void SetColor() {
		color = GetComponent<GameController>().gameManager.GetComponent<GameManager>().GetPlayerColor(playerIndex);
	}

	void UpdateSeatNo(int _seatNo) {
		if (_seatNo >= 0) {
			GetComponent<PlayerUIController>().UpdateSeatNo(_seatNo);
		}
	}

	public void SetPlayerIndex() {
		playerIndex = GameManager.GetPlayers().Count() - 1;
	}
		
	public void SetScore(int _score) {
		score += _score;
	}

	public void SetSeat(int _seatNo) {
		seatNo = _seatNo;
	}

	public void SetIndex(int _index) {
		seatIndex = _index;
	}


	public void SetupPlayer() {
		// color
		//seatNo

		seatNo = GameManager.GetEmptySeat();
		string _id = GameManager.GetEmptyId().ToString();
		GameManager.RegisterPlayer(_id, GetComponent<Player>());
		GameManager.instance.fakePlayers++;

	}
}
