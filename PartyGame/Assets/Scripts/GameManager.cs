﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class GameManager : NetworkBehaviour {

	[SerializeField] Player playerPrefab;
	[SerializeField] Color[] playerColors;

	public static GameManager instance;
	public MatchSettings matchSettings;

	public int fakePlayers;


	void Awake() {
		if (instance != null) {
			Destroy(instance.gameObject);
			instance = this;
		} else {
			instance = this;
		}
	}

	void Start() {
		DontDestroyOnLoad(gameObject);
		fakePlayers = 0;
	}

	public Color GetPlayerColor(int _index) {
		return playerColors[_index];
	}

	public void CreateNewPlayer() {
		Player _player = Instantiate(playerPrefab);
		_player.GetComponent<Player>().SetupPlayer();
	}
		
	#region Player Register

	private const string PLAYER_ID_PREFIX = "Player ";

	private static Dictionary<string, Player> players = new Dictionary<string, Player>();

	public static void RegisterPlayer(string _netID, Player _player) {
		string _playerID = PLAYER_ID_PREFIX + _netID;
		players.Add(_playerID, _player);
		_player.transform.name = _playerID;
	}

	public static void UnregisterPlayer(string _playerID) {
		players.Remove(_playerID);
	}

	public static Player GetPlayer(string _playerID) {
		return players[_playerID];
	}

	public static Player[] GetPlayers() {
		return players.Values.ToArray();
	}

	public static Player GetLocalPlayer() {
		foreach(Player _player in players.Values) {
			if (_player.isLocalPlayer) {
				return _player;
			}
		}
		return null;
	}

	public static Player[] GetPlayersWhoNeedASeat() {
		List<Player> _playerArray = new List<Player>();
		foreach (Player _player in players.Values) {
			if (_player.seatNo == 0) {
				_playerArray.Add(_player);
			}
		}
		return _playerArray.ToArray();
	}

	public static void ResetAll() {
		players.Clear();
	}

	#endregion

	public int GetPlayersSeated() {
		int _no = 0;
		foreach (Player _player in players.Values) {
			if (_player.seatNo != 0) {
				_no++;
			}
		}
		return _no;
	}

	public static Player GetPlayerBySeat(int _seatNo) {
		foreach (Player _player in GetPlayers()) {
			if (_player.seatNo == _seatNo) {
				return _player;
			}
		}
		return null;
	}

	public static Player[] GetPlayersSortedBySeat() {
		Dictionary<Player, int> _playerDic = new Dictionary<Player, int>();
		foreach (Player _player in players.Values) {
			_playerDic.Add(_player, _player.seatNo);
		}
		_playerDic = _playerDic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
		return _playerDic.Keys.ToArray();
	}

	public static void RearrangePlayersSortedBySeat() {
		Dictionary<Player, int> _playerDic = new Dictionary<Player, int>();
		foreach (Player _player in players.Values) {
			_playerDic.Add(_player, _player.seatNo);
		}
		_playerDic = _playerDic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

		Dictionary<string, Player> _newPlayerDic = new Dictionary<string, Player>();
		foreach (Player _player in _playerDic.Keys) {
			_newPlayerDic.Add(_player.name, _player);
		}
		players = _newPlayerDic;

	}

	public static int GetEmptySeat() {
		for(int i = 0; i < 4; i++) {
			if (GetPlayerBySeat(i) == null) {
				return i;
			}
		}
		return -1;
	}

	public static int GetEmptyId() {
		return GameManager.instance.fakePlayers + 100;
	}


}
