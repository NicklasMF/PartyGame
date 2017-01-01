using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour {

	public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
	private JoinRoomDelegate joinRoomCallback;

	[SerializeField] private Text roomNameText;

	private MatchInfoSnapshot match;

	public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback) {
		match = _match;
		joinRoomCallback = _joinRoomCallback;
		int matchSize = match.currentSize;

		roomNameText.text = match.name + " (" + matchSize + "/" + match.maxSize + ")";

	}

	public void JoinGame() {
		joinRoomCallback.Invoke(match);
	}
}
