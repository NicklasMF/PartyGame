using UnityEngine;
using System.Collections;

public class SwipeTheBomb_SceneManager: MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Player _player in GameManager.GetPlayers()) {
			_player.gameObject.GetComponent<SwipeTheBomb_Player>().enabled = true;
		}
		GameManager.GetServer().gameObject.GetComponent<SwipeTheBomb_Player>().enabled = true;
	}

}
