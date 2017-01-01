using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {

	[Header("UI Scenes")]
	public GameObject uiSeatings;
	public GameObject uiResults;
	public GameObject uiNewRule;
	public GameObject uiNextGame;
	public GameObject uiSettings;

	public List<GameObject> uiList = new List<GameObject>();


	void Start() {
		GenerateUIArray();
	}

	void GenerateUIArray() {
		uiList.Add(uiSeatings);
		uiList.Add(uiResults);
		uiList.Add(uiNewRule);
		uiList.Add(uiNextGame);
		uiList.Add(uiSettings);
	}


}
