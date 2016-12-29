using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameUI : NetworkBehaviour {

	[Header("UI Scenes")]
	public GameObject uiSeatings;
	public GameObject uiResults;
	public GameObject uiNewRule;
	public GameObject uiNextGame;
	public GameObject uiSettings;

	private List<GameObject> uiList = new List<GameObject>();


	void Start() {
		GenerateUIArray();
		ShowSeatings();
	}

	void GenerateUIArray() {
		uiList.Add(uiSeatings);
		uiList.Add(uiResults);
		uiList.Add(uiNewRule);
		uiList.Add(uiNextGame);
		uiList.Add(uiSettings);
	}


	#region UI Navigation

	void DeactiveUI() {
		foreach (GameObject ui in uiList) {
			ui.SetActive(false);
		}
	}

	public void ShowSeatings() {
		DeactiveUI();
		uiSeatings.SetActive(true);
	}

	public void ShowResults() {
		DeactiveUI();
		uiResults.SetActive(true);
	}

	public void ShowNewRule() {
		DeactiveUI();
		uiNewRule.SetActive(true);
	}

	public void ShowNextGame() {
		DeactiveUI();
		uiNextGame.SetActive(true);
		uiNextGame.GetComponent<UI_NextGame>().SetNextGame();
	}

	public void ShowSettings() {
		DeactiveUI();
		uiSettings.SetActive(true);
	}

	#endregion

}
