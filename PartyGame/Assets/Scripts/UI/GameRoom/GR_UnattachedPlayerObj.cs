using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GR_UnattachedPlayerObj : MonoBehaviour {

	public Text txtPlayerName;
	public Image imgBackground;
	public Player player;

	public void Setup(string _name, Color _color, Player _player) {
		txtPlayerName.text = _name;

		Image backgroundImg = imgBackground.GetComponent<Image>();
		Color _newColor = new Color(_color.r,_color.g, _color.b, 1f);
		backgroundImg.color = _newColor;

		player = _player;
	}
}
