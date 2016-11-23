using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GR_PlayerArea : MonoBehaviour {

	public int seatNo;
	[SerializeField] GameObject preGameUIPrefab;

	void Start() {
		GetComponent<Button>().onClick.AddListener(ButtonClick);
	}

	void ButtonClick() {
		preGameUIPrefab.GetComponent<PregameUI>().SetPlayer(gameObject, seatNo);
	}

}
