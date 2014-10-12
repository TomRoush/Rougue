using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void OnGUI()
	{

		if (GUI.Button (new Rect (175, 235, 100, 30), "NEW GAME")) 
		{
			Application.LoadLevel ("TileMapTester");
		}
		if (GUI.Button (new Rect (175, 285, 100, 30), "LOAD GAME"))
		{
			Application.LoadLevel ("TileMapTester");
		}
		if (GUI.Button (new Rect (175, 335, 100, 30), "SETTINGS")) 
		{
		}
		if (GUI.Button (new Rect (Screen.width-130, 350, 75, 30), "QUIT")) 
		{
			Application.Quit ();
		}
	}

}
