﻿using UnityEngine;
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

	[SerializeField] public GameObject gameManager;

	public Color color;
	private int playerIndex;

	void Start() {

		SetColor();

		if (isLocalPlayer) {
			SetPlayerIndex();
		}
	}

	void SetColor() {
		color = gameManager.GetComponent<GameManager>().GetPlayerColor(playerIndex);
	}

	public void SetPlayerIndex() {
		playerIndex = GameManager.GetPlayers().Count() - 1;
	}
		
	public void SetScore(int _score) {
		score += _score;
	}
}
