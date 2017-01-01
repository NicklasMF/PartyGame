using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerUIController : NetworkBehaviour {

	GameObject gameUI;

	public void SetUI(GameObject _ui) {
		gameUI = _ui;
	}

	void Start() {
		foreach(GameObject go in gameUI.GetComponent<GameUI>().uiSeatings.GetComponent<UI_Seatings>().PlayerSeatArray) {
			go.GetComponent<Button>().onClick.AddListener(ButtonClick);
		}
		//gameUI.GetComponent<GameUI>().uiSeatings.GetComponent<UI_Seatings>().PlayerSeatArray[]
	}

	void Update() {
		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				Debug.Log( hit.transform.gameObject.name );
			}
		}
	}

	#region UI Navigation

	void DeactiveUI() {
		foreach (GameObject ui in gameUI.GetComponent<GameUI>().uiList) {
			ui.SetActive(false);
		}
	}

	public void ShowSeatings() {
		DeactiveUI();
		gameUI.GetComponent<GameUI>().uiSeatings.SetActive(true);
	}

	[ClientRpc]
	public void RpcShowSeatings() {
		if (isLocalPlayer) {
			DeactiveUI();
			gameUI.GetComponent<GameUI>().uiSeatings.SetActive(true);
		}
	}

	public void ShowResults() {
		DeactiveUI();
		gameUI.GetComponent<GameUI>().uiResults.SetActive(true);
	}

	public void ShowNewRule() {
		DeactiveUI();
		gameUI.GetComponent<GameUI>().uiNewRule.SetActive(true);
	}

	[Command]
	public void CmdShowNextGame() {
		foreach (Player player in GameManager.GetPlayers()) {
			player.GetComponent<PlayerUIController>().RpcShowNextGame();
		}
	}

	[ClientRpc]
	public void RpcShowNextGame() {
		if (isLocalPlayer) {
			DeactiveUI();
			gameUI.GetComponent<GameUI>().uiNextGame.SetActive(true);
			gameUI.GetComponent<GameUI>().uiNextGame.GetComponent<UI_NextGame>().SetNextGame();
		}
	}

	public void ShowSettings() {
		DeactiveUI();
		gameUI.GetComponent<GameUI>().uiSettings.SetActive(true);
	}

	#endregion

	#region Seating

	void ButtonClick() {
		//CmdPickSeat();
		//GameManager.GetLocalPlayer().GetComponent<Player>().SetSeat(seatNo);
		//SetupSeat();
		Debug.Log("Click");
	}
		
	[Command]
	public void CmdPickSeat(int _seat) {
		/*foreach (Player player in GameManager.GetPlayers()) {
			player.GetComponent<PlayerUIController>().RpcPickSeat(_seat);
		}*/
		GetComponent<Player>().SetSeat(_seat);
	}

	public void UpdateSeatNo(int _seatNo) {
		gameUI.GetComponent<GameUI>().uiSeatings.GetComponent<UI_Seatings>().PlayerSeatArray[_seatNo].GetComponent<GR_PlayerArea>().SetupSeat();

	}

	#endregion

}
