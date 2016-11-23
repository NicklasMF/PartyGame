using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

// Class from Player Object //
public class GameController : NetworkBehaviour {

	[SerializeField] public GameObject gameManager;
	[SerializeField] GameObject[] sceneManagers;
	[SerializeField] MonoBehaviour[] gameScripts;
	[SerializeField] GameObject pregameUIPrefab;
	[SerializeField] GameObject playerUIPrefab;

	public GameObject pregameUI;
	public GameObject playerUI;
	private string sceneName;

	void Start() {
		OnStartGame();

		foreach (MonoBehaviour _comp in gameScripts) {
			_comp.enabled = false;
		}
	}

	void EnableScript(string _name) {
		foreach (MonoBehaviour _comp in gameScripts) {
			if (_comp.name == _name) {
				_comp.enabled = true;
			}
		}
	}

	[ClientRpc]
	public void RpcGoToScene(string _scenename) {
		if (isLocalPlayer) {
			SceneManager.LoadScene(_scenename);
			//OnNewLevel(_scene, _mode);
		}
	}

	[Command]
	public void CmdGoToScene(string _scenename) {
		if (isLocalPlayer) {
			SceneManager.LoadScene(_scenename);
		}
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnNewLevel;
	}



	void OnStartGame() {
		if (isLocalPlayer) {
			if (transform.GetComponent<PlayerSetup>().isTheServer) {
				pregameUI = Instantiate(pregameUIPrefab) as GameObject;
			} else {
				string _name = transform.name;
				Color _color = transform.GetComponent<Player>().color;
				playerUI = Instantiate(playerUIPrefab) as GameObject;
				playerUI.GetComponent<UIPlayer>().SetWaitingScreen(_name, _color);
			}

		}

	}

	void OnNewLevel(Scene _scene, LoadSceneMode mode) {
		if (isLocalPlayer) {
			string sceneManagerName = _scene.name + "_SceneManager";
			Debug.Log(sceneManagerName);
			GameObject sceneManager = Instantiate(GetSceneManager(sceneManagerName)) as GameObject;

			if (transform.GetComponent<PlayerSetup>().isTheServer) {
				// Server
				pregameUIPrefab = sceneManager.GetComponent<TheSceneManager>().uIServer;
				pregameUI = Instantiate(pregameUIPrefab) as GameObject;
			} else {
				// Client
				string _name = transform.name;
				Color _color = transform.GetComponent<Player>().color;
				playerUIPrefab = sceneManager.GetComponent<TheSceneManager>().uIPlayer;
				playerUI = Instantiate(playerUIPrefab) as GameObject;
			}
		}
	}

	GameObject GetSceneManager(string _name) {
		foreach (GameObject _sm in sceneManagers) {
			if (_sm.name == _name) {
				return _sm;
			}
		}
		return null;
	}

}
