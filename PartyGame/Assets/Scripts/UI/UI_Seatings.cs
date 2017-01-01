using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UI_Seatings : NetworkBehaviour {

	public GameObject[] PlayerSeatArray;

	public Button btnStartGame;

	void Start() {
		btnStartGame.onClick.AddListener(StartGame);
	}

	public void StartGame() {
		// Rearrange Players //
		//GameManager.RearrangePlayersSortedBySeat();

		GameManager.GetLocalPlayer().GetComponent<PlayerUIController>().CmdShowNextGame();

	}

}
