using UnityEngine;
using System.Collections;

public class UI_Results : MonoBehaviour {

	[SerializeField] GameObject playerResultItemPrefab;

	public GameObject playerContent;

	void Start() {
		GameObject _item = Instantiate(playerResultItemPrefab, playerContent.transform, false) as GameObject;
		_item.GetComponent<PlayerResultItem>().Setup(Color.red, "Nicklas", 3);

		GameObject _item1 = Instantiate(playerResultItemPrefab, playerContent.transform, false) as GameObject;
		_item1.GetComponent<PlayerResultItem>().Setup(Color.yellow, "Trine", 2);
		}

}
