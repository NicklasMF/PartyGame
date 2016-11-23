using UnityEngine;
using UnityEngine.UI;

public class UIWaitingScreen : MonoBehaviour {

	[SerializeField] Image background;
	[SerializeField] Text playername;

	public void UpdateScreen(string _name, Color _color) {
		playername.text = _name;
		Color _newColor = new Color(_color.r,_color.g, _color.b, 0.8f);
		background.color = _newColor;
	}
	
}
