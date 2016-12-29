using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UI_Seatings : NetworkBehaviour {

	public Button btnStartGame;

	void Start() {
		btnStartGame.onClick.AddListener(StartGame);
	}

	public void StartGame() {
		// Rearrange Players //
		//GameManager.RearrangePlayersSortedBySeat();

		GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>().ShowNextGame();

	}



}
