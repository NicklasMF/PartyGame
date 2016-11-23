using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlayer : MonoBehaviour {

	public GameObject waitingUI;

	public void SetWaitingScreen(string _name, Color _color) {
		waitingUI.GetComponent<UIWaitingScreen>().UpdateScreen(_name, _color);
	}

	public void ShowWaitingScreen() {

	}

}
