using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GR_PlayerArea : MonoBehaviour {

	public int seatNo;
	[SerializeField] GameObject preGameUIPrefab;
	[SerializeField] GameObject seatGO;
	[SerializeField] Text nameTxt;

	void Start() {
		//GetComponent<Button>().onClick.AddListener(ButtonClick);
	}


	public void SetupSeat() {
		
		Color _color = GameManager.GetPlayerBySeat(seatNo).color;
		seatGO.GetComponent<Image>().color = _color;
		nameTxt.text = GameManager.GetPlayerBySeat(seatNo).name;
	}

}
