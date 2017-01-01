using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerResultItem : MonoBehaviour {

	[SerializeField] Image background;
	[SerializeField] Text txtPlayerName;
	[SerializeField] Text txtDrinkCount;
	[SerializeField] Text txtDrinkZips;

	[SerializeField] GameObject drinkImgWrapper;
	[SerializeField] GameObject drinkPrefab;

	[SerializeField] Image imgRule;
	[SerializeField] Text txtRule;

	public void Setup(Player _player) {
		
	}

	public void Setup(Color _color, string _name, int _drinkCount) {
		background.color = _color;
		txtPlayerName.text = _name;
		txtDrinkCount.text = _drinkCount.ToString();
		StartCoroutine(AnimateItem(_drinkCount));
	}

	void Start() {
		txtPlayerName.gameObject.SetActive(false);
		txtDrinkCount.gameObject.SetActive(false);
		txtRule.gameObject.SetActive(false);
		txtDrinkZips.gameObject.SetActive(false);
		imgRule.gameObject.SetActive(false);
	}

	void Update() {
	}


	IEnumerator AnimateItem(int _drinks) {
		yield return new WaitForSeconds(0.5f);

		txtPlayerName.gameObject.SetActive(true);
		float secs = 1f / _drinks;

		for (int i = 0; i < _drinks; i++) {
			yield return new WaitForSeconds(secs);

			Instantiate(drinkPrefab, drinkImgWrapper.transform, false);
		}

		yield return new WaitForSeconds(1f);

		txtDrinkCount.gameObject.SetActive(true);
		txtDrinkZips.gameObject.SetActive(true);

	}


}
