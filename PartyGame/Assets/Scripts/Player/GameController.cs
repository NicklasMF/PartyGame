using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

// Class from Player Object //
public class GameController : NetworkBehaviour {

	// GameThings
	[SyncVar] int gamesPlayed = 0;


	// Behind The Scenes //
	[SerializeField] public GameObject gameManager;
	[SerializeField] Behaviour[] gameScripts;
	[SerializeField] GameObject gameUIPrefab;

	private static Dictionary<int, string> allMiniGames = new Dictionary<int, string>();
	public static Dictionary<int, int> gamesDict = new Dictionary<int, int>();
	public GameObject gameUI;
	private GameObject playerUIPrefab;
	public GameObject playerUI;
	private string sceneName;

	void GenerateDictionary() {
		allMiniGames.Add(0, "SwipeTheBomb");

		GenerateGameDict(1);
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

	[Command]
	public void CmdGoToScene(string _scenename) {
		if (isLocalPlayer) {
			SceneManager.LoadScene(_scenename);
		}
	}
/*	[Command]
	public void CmdGoToScene(string scenename) {
		foreach (Player _player in GameManager.GetPlayers()) {
			_player.GetComponent<GameController>().RpcGoToScene(scenename);
		}
	}
*/
	[ClientRpc]
	public void RpcGoToScene(string _scenename) {
		if (isLocalPlayer) {
			SceneManager.LoadScene(_scenename);
			//OnNewLevel(_scene, _mode);
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
			gameUI = Instantiate(gameUIPrefab) as GameObject;
		}
	}

	void OnNewLevel(Scene _scene, LoadSceneMode mode) {
		if (_scene.name != "GameRoom") {
			int scriptIndex = GetMiniGame(_scene.name);
			//EnableScript(gameScripts[scriptIndex]);

			EnableScript(scriptIndex);
			if (isLocalPlayer) {
				GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
				playerUIPrefab = sceneManager.GetComponent<TheSceneManager>().uIPlayer;
				playerUI = Instantiate(playerUIPrefab) as GameObject;
			}
		} else {
			gamesPlayed++;
		}
	}
		
	public static int GetMiniGame(string _name) {
		return allMiniGames.FirstOrDefault(x => x.Value == _name).Key;
	}

	public static string GetMiniGame(int _index) {
		return allMiniGames[_index];
	}

	public int GetNextGameIndex() {
		return gamesDict[gamesPlayed + 1];
	}

	void GenerateGameDict(int gameCount) {
		for (int i = 1; i <= gameCount; i++) {
			int gameNo = Random.Range(0, allMiniGames.Count - 1);
			gamesDict.Add(i, gameNo);
		}
	}

}
