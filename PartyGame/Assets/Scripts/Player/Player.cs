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

	public Color color;
	public int seatNo;
	public int seatIndex;
	private int playerIndex;

	void Start() {
		DontDestroyOnLoad(gameObject);

		SetColor();

		if (isLocalPlayer) {
			SetPlayerIndex();
			seatNo = 0;
		}
	}

	void SetColor() {
		color = GetComponent<GameController>().gameManager.GetComponent<GameManager>().GetPlayerColor(playerIndex);
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
}
