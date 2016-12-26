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
	[SerializeField] Behaviour[] gameScripts;
	[SerializeField] GameObject pregameUIPrefab;
	[SerializeField] GameObject playerUIPrefab;

	public static Dictionary<string, int> miniGames = new Dictionary<string, int>();
	public GameObject pregameUI;
	public GameObject playerUI;
	private string sceneName;

	void GenerateDictionary() {
		miniGames.Add("SwipeTheBomb", 0);
	}

	void Start() {
		OnStartGame();
		foreach (MonoBehaviour _comp in gameScripts) {
			_comp.enabled = false;
		}

		if (isLocalPlayer) {
			GenerateDictionary();
		}
	}

	void EnableScript(string _name) {
		foreach (Behaviour _comp in gameScripts) {
			if (_comp.name == _name) {
				_comp.enabled = true;
			}
		}
	}

	void EnableScript(int _index) {
		gameScripts[_index].enabled = true;
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

	void OnDisable() {
		SceneManager.sceneLoaded -= OnNewLevel;		
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
		int scriptIndex = GetMiniGame(_scene.name);
		//EnableScript(gameScripts[scriptIndex]);
		EnableScript(scriptIndex);
		if (isLocalPlayer) {
			string sceneManagerName = _scene.name + "_SceneManager";
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

	public static int GetMiniGame(string _name) {
		foreach(string key in miniGames.Keys){
			if(key == _name){
				return miniGames[key];
			} 
		}
		return -1;
	}

}
