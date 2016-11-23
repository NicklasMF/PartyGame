using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeTheBomb_PlayerUI : MonoBehaviour {

	[SerializeField] Image background;
	[SerializeField] Text txtPoint;
	[SerializeField] Image bomb;

	void Start() {
		bomb.enabled = false;
	}

	void Setup(Color _color) {
		txtPoint.text = "0";
	}

	public void SetBomb(string _name) {
		txtPoint.text = _name;
	}

	public void HasTheBomb(bool _bool) {
		bomb.enabled = _bool;
		Debug.Log("Has the bomb: "+_bool);
	}


}
