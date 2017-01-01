using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_NextGame : MonoBehaviour {

	[SerializeField] Text txtGameTitle;
	[SerializeField] Image imgGame;
	[SerializeField] Text txtTapForNext;

	string nextGameTitle;
	bool readyToGo;

	void Start() {
		readyToGo = false;
		txtTapForNext.gameObject.SetActive(false);

		StartCoroutine(WaitToBeReady(2));
	}

	void Update() {
		
		if (Input.GetMouseButtonDown(0) && readyToGo) {
			SetReady();
		}
	}
		
	public void SetNextGame() {
		Debug.Log("Setting up Next Game");
		int nextGameIndex = GameManager.GetLocalPlayer().GetComponent<GameController>().GetNextGameIndex();
		nextGameTitle = GameController.GetMiniGame(nextGameIndex);
		txtGameTitle.text = nextGameTitle;
	}

	public void SetReady() {
		// Set Ready

		GameManager.GetLocalPlayer().GetComponent<GameController>().CmdGoToScene(nextGameTitle);
	}

	IEnumerator WaitToBeReady(int _seconds) {
		yield return new WaitForSeconds(_seconds);

		readyToGo = true;
		txtTapForNext.gameObject.SetActive(true);
	}

}
