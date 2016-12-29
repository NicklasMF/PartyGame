using UnityEngine;
using System.Collections;

public class PlayerItem_Placeholder : MonoBehaviour {

	public int placeholderIndex;
	private GameObject canvasItem;

	void Start() {
		if (GameManager.GetPlayerBySeat(placeholderIndex) != null) {
			Player player = GameManager.GetLocalPlayer();
			GameObject playerUI = player.GetComponent<GameController>().playerUI;
			GameObject itemPrefab = playerUI.GetComponent<SwipeTheBomb_PlayerUI>().playerCanvasItem;

			canvasItem = (GameObject) Instantiate(itemPrefab, transform, false);
			canvasItem.transform.localScale = transform.localScale;
		}
	}

}
