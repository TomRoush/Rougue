using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private Texture2D new_game;
	private Texture2D load_game;
	private Texture2D settings;

	public void Start() {

		new_game = Resources.Load ("Artwork/Main_Menu/new_game") as Texture2D;
		load_game = Resources.Load ("Artwork/Main_Menu/load_game") as Texture2D;
		settings = Resources.Load ("Artwork/Main_Menu/settings") as Texture2D;

	}

	public void OnGUI()
	{
		if (GUI.Button (new Rect ((Screen.width)/5+15, (Screen.height)/2+15, 100, 50), new_game, ""))
		{
			Application.LoadLevel ("TileMapTester");
		}
		if (GUI.Button (new Rect ((Screen.width)/5+15, (Screen.height)/2+65, 100, 50), load_game, ""))
		{
			Application.LoadLevel ("TileMapTester");
		}
		if (GUI.Button (new Rect ((Screen.width)/5+15, (Screen.height)/2+115, 100, 50), settings, ""))
		{
			Application.LoadLevel ("TileMapTester");
		}

	}
}