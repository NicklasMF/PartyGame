using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class PregameUI : NetworkBehaviour {

	[SerializeField] GameObject pregamePlayerItem;
	[SerializeField] float secondsBetweenUpdate;
	[SerializeField] Transform playerList;
	[SerializeField] GameObject gr_CenterArea;
	[SerializeField] GameObject gr_CenterAreaPregame;
	[SerializeField] GameObject gr_AttachedPlayerObj;

	public Button btnStartGame;

	Player[] arr;

	void Start() {
		RefreshPlayerlist();
		btnStartGame.onClick.AddListener(CmdStartGame);
	}

	public void RefreshPlayerlist () {
		RemovePlayers();
		AddPlayers();
		Invoke("RefreshPlayerlist", secondsBetweenUpdate);
	}

	void AddPlayers() {
		GameManager.RearrangePlayersSortedBySeat();

		Player[] players = GameManager.GetPlayersWhoNeedASeat();
		Player[] playersTotal = GameManager.GetPlayers();

		foreach (Player player in players) {
			GameObject itemGO = Instantiate(pregamePlayerItem) as GameObject;
			itemGO.transform.SetParent(playerList, false);
			GR_UnattachedPlayerObj item = itemGO.GetComponent<GR_UnattachedPlayerObj>();

			if (item != null) {
				Color _color = player.color;
				item.Setup(player.username, _color, player);
			}
		}

		//if (playersTotal.Length <= 1) {
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().ShowButton(true);
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().ShowText(true);
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().SetText("Lad os vente på flere spillere...");
		/*} else if (playersTotal.Length < 4) {
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().ShowButton(true);
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().ShowText(true);
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().SetText("... eller skal vi vente på flere spillere?");
		} else {
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().ShowButton(true);
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().ShowText(false);
			gr_CenterAreaPregame.GetComponent<GR_Pregame>().SetText("Så er vi klar til at spille!");
		}*/
	}

	/*void SetPlayer() {
		GameManager.UnsetPlayerWhoNeedASeat()
	}
	/*void AddPlayers() {
		Player[] players = GameManager.GetPlayers();

		foreach (Player player in players) {
			GameObject itemGO = Instantiate(pregamePlayerItem) as GameObject;
			itemGO.transform.SetParent(playerList, false);
			GR_UnattachedPlayerObj item = itemGO.GetComponent<GR_UnattachedPlayerObj>();

			if (item != null) {
				Color _color = player.color;
				item.Setup(player.username, _color);
			}
		}
	}*/

	void RemovePlayers() {
		foreach(Transform child in playerList) {
			Destroy(child.gameObject);
		}
	}

	public void SetPlayer(GameObject _area, int _seatNo) {
		int _playerCount = playerList.childCount;
		if (_playerCount > 0) {
			Transform _obj = playerList.GetChild(_playerCount - 1);
			_obj.GetComponent<GR_UnattachedPlayerObj>().player.SetSeat(_seatNo);
			GameObject _playerObj = Instantiate(gr_AttachedPlayerObj, _area.transform, false) as GameObject;
			if (_playerObj != null) {
				string _name = _obj.GetComponent<GR_UnattachedPlayerObj>().player.name;
				Color _color = _obj.GetComponent<GR_UnattachedPlayerObj>().player.color;
				Player _player = _obj.GetComponent<GR_UnattachedPlayerObj>().player;

				_playerObj.GetComponent<GR_AttachedPlayerObj>().Setup(_name, _color, _player);
			} else {
				Debug.Log("Spillere blev ikke sat op");
			}
		} else {
			Debug.Log("Ingen spillere at sætte på pladsen");
		}
	}

	[Command]
	public void CmdStartGame() {
		// Rearrange Players //
		GameManager.RearrangePlayersSortedBySeat();

		string newScene = "SwipeTheBomb";
		foreach (Player _player in GameManager.GetPlayers()) {
			_player.GetComponent<GameController>().RpcGoToScene(newScene);
		}
		Player _server = GameManager.GetServer();

		_server.GetComponent<GameController>().CmdGoToScene(newScene);
		//SceneManager.LoadScene(newScene);

	}

}
