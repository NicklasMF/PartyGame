using UnityEngine;
using UnityEngine.UI;

public class GetReadyUI : MonoBehaviour {

	private GameObject imageTarget;

	public static bool IsOn = false;
	public bool isReady = false;

	void Start() {
		IsOn = false;
		isReady = false;
	}

	void Update() {
		if (isReady) {
			return;
		}
	}

}
