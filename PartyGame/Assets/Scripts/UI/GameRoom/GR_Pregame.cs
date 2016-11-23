using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GR_Pregame : MonoBehaviour {

	[SerializeField] Image background;
	[SerializeField] GameObject btnStartGame;
	public Text txtWaitForOthers;

	public void ShowButton(bool _bool) {
		btnStartGame.SetActive(_bool);
	}

	public void ShowText(bool _bool) {
		txtWaitForOthers.enabled = _bool;
	}

	public void SetText(string _text) {
		txtWaitForOthers.text = _text;
	}


}
