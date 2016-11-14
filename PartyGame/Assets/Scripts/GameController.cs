using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class GameController : NetworkBehaviour {

	[SerializeField] GameObject gameManager;

	[SyncVar] public bool gameStarted = false;


	void Start() {
		gameStarted = false;
	}

}
