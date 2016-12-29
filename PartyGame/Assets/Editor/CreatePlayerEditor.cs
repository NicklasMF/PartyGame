using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameManager))]
public class CreatePlayerEditor : Editor {

	public Player playerPrefab;

	public override void OnInspectorGUI() {
		Debug.Log("Create a new player");

		DrawDefaultInspector();

		//ObjectBuilderScript myScript = (ObjectBuilderScript)target;
		if(GUILayout.Button("Create Player"))
		{
			GameManager.instance.CreateNewPlayer();
		}


	}
}
