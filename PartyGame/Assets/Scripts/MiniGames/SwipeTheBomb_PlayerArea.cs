using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeTheBomb_PlayerArea : MonoBehaviour {

	public int seatNo;

	[SerializeField] Image background;
	[SerializeField] Text txtPlayername;
	[SerializeField] Text txtPoint;

	public void Setup(Player _player) {
		Color _color = _player.color;
		Image backgroundImg = background.GetComponent<Image>();
		Color _newColor = new Color(_color.r,_color.g, _color.b, 1f);
		backgroundImg.color = _newColor;

		txtPlayername.text = _player.name;
		txtPoint.text = "0";
	}

	public void SetPoint(bool _bool) {
		// Has the bomb //
		if (_bool) {
			txtPoint.text = "1";
		} else {
			txtPoint.text = "0";
		}
	}

}
