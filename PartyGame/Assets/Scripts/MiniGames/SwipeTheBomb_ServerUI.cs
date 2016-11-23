using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeTheBomb_ServerUI : MonoBehaviour {

	public GameObject[] playerAreas;
	public Button startGameBtn;

	void Start() {
		startGameBtn = transform.Find("Button").gameObject.transform.GetComponent<Button>();
		Setup();
	}

	void Update() {
		UpdateUI();
	}

	void Setup() {
		foreach (Player _player in GameManager.GetPlayersSortedBySeat()) {
			int _index = _player.seatNo - 1;
			playerAreas[_index].GetComponent<SwipeTheBomb_PlayerArea>().Setup(_player);
		}
	}

	void UpdateUI() {
		foreach (Player _player in GameManager.GetPlayers()) {
			SetPoint(_player);
		}
	}

	void SetPoint(Player _player) {
		bool _bool;
		int _index =_player.seatNo - 1;
		if (_player.GetComponent<SwipeTheBomb_Player>().haveTheBombIndex == _player.seatNo) {
			_bool = true;
		} else {
			_bool = false;
		}
		playerAreas[_index].GetComponent<SwipeTheBomb_PlayerArea>().SetPoint(_bool);
	}
}
