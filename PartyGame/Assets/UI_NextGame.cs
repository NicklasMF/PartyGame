using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_NextGame : MonoBehaviour {

	[SerializeField] Text txtGameTitle;
	[SerializeField] Image imgGame;
	[SerializeField] Text txtTapForNext;

	void Start() {
		
		txtTapForNext.gameObject.SetActive(false);
	}
		
	public void SetNextGame() {
		Debug.Log("Setting up Next Game");
		int nextGameIndex = GameManager.GetLocalPlayer().GetComponent<GameController>().GetNextGameIndex();
		Debug.Log(nextGameIndex);
		string nextGameTitle = GameController.GetMiniGame(nextGameIndex);
		txtGameTitle.text = nextGameTitle;
	}

}
