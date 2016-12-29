using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GR_PlayerArea : MonoBehaviour {

	public int seatNo;
	[SerializeField] GameObject preGameUIPrefab;
	[SerializeField] GameObject seatGO;
	[SerializeField] Text nameTxt;

	void Start() {
		GetComponent<Button>().onClick.AddListener(ButtonClick);
	}

	void ButtonClick() {
		//preGameUIPrefab.GetComponent<PregameUI>().SetPlayer(gameObject, seatNo);
		GameManager.GetLocalPlayer().GetComponent<Player>().SetSeat(seatNo);
		SetupSeat();
	}

	void SetupSeat() {
		Color _color = GameManager.instance.GetPlayerColor(seatNo);
		seatGO.GetComponent<Image>().color = _color;
		nameTxt.text = GameManager.GetLocalPlayer().GetComponent<Player>().name;
	}

}
