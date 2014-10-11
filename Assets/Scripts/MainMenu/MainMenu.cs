using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void OnGUI()
	{
		if (GUI.Button (new Rect (50, 50, 75, 50), "PLAY")) 
		{
			Application.LoadLevel ("TileMapTester");
		}
	}

}
