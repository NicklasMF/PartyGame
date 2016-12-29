using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField] Behaviour[] componentsToDisable;
	[SerializeField] string remoteLayerName = "RemotePlayer";
	[SerializeField] GameObject playerGraphics;

	void Start() {
		//Transform _playerFolder = _imageTarget.transform.FindDeepChild("Players");
		//gameObject.transform.SetParent(_playerFolder);

		string _username = "Loading...";
		_username = transform.name;
		CmdSetUsername(transform.name, _username);
	
		if (!isLocalPlayer) {
			AssignRemoteLayer();
		} else {
			
		}
		//playerGraphics.GetComponent<Renderer>().material.color = GetComponent<Player>().color;

	}

	public override void OnStartClient() {
		base.OnStartClient();

		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		Player _player = GetComponent<Player>();
		GameManager.RegisterPlayer(_netID, _player);
		GetComponent<Player>().SetPlayerIndex();

	}

	void RegisterPlayer() {
		string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
		transform.name = _ID;
	}

	void SetLayerRecursively(GameObject obj, int newLayer) {
		obj.layer = newLayer;

		foreach(Transform child in obj.transform) {
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	void AssignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	[Command]
	void CmdSetUsername (string _playerID, string _username) {
		Player player = GameManager.GetPlayer(_playerID);
		if (player != null) {
			player.username = _username;
		}
	}

	void OnDisable() {
		/*if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}*/

		GameManager.UnregisterPlayer(transform.name);
	}
}


