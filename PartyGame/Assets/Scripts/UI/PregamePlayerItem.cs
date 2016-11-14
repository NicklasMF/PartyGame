using UnityEngine;
using UnityEngine.UI;

public class PregamePlayerItem : MonoBehaviour {

	[SerializeField] Text usernameText;
	[SerializeField] GameObject background;

	public void Setup(string _username, Color _color) {
		usernameText.text = _username;

		Image backgroundImg = background.GetComponent<Image>();
		Color _newColor = new Color(_color.r,_color.g, _color.b, 0.4f);
		backgroundImg.color = _newColor;

	}

}
