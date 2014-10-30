using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private enum Menu { MAIN, SETTINGS, LOAD };
	private static Menu scene = Menu.MAIN;
	
	private AudioClip sheath;

	private Texture2D new_game;
	private Texture2D load_game;
	private Texture2D settings;
	private Texture2D quit;

	public float volume = 0.0f;

	public void Start() 
	{
		new_game = Resources.Load ("Artwork/Main_Menu/new_game") as Texture2D;
		load_game = Resources.Load ("Artwork/Main_Menu/load_game") as Texture2D;
		settings = Resources.Load ("Artwork/Main_Menu/settings") as Texture2D;
		quit = Resources.Load ("Artwork/Main_Menu/quit") as Texture2D;
		
	}

	public void Update() {
		if(Input.GetMouseButtonDown(0)) 
		{
			audio.PlayOneShot(sheath);
			Debug.Log ("Mouse pressed.");
		}

		AudioListener.volume = volume / 10;
	}

	public void OnGUI()
	{
		if(scene == Menu.MAIN) 
		{
			if (GUI.Button (new Rect ((Screen.width)/17, (Screen.height)/2, 200, 100), new_game, ""))
			{
				Application.LoadLevel ("Game");
			}
			if (GUI.Button (new Rect ((Screen.width)/17, (3*(Screen.height))/4, 200, 100), settings, ""))
			{
				scene = Menu.SETTINGS;
			}
			if (GUI.Button (new Rect ((Screen.width)/17, (5*(Screen.height))/8, 200, 100), load_game, ""))
			{
				Application.LoadLevel ("Game");
			}
			if (GUI.Button (new Rect (Screen.width-125, Screen.height-Screen.height/8-15, 75, 30), quit, ""))
			{
				Application.Quit ();
			}
			
		} else if(scene == Menu.SETTINGS)
		{
			volume = GUI.HorizontalSlider (new Rect ((Screen.width)/5+15, (Screen.height)/2+20, 100, 50), volume, 0.0f, 10.0f);

			audio.volume = this.volume;

			GUI.Label (new Rect ((Screen.width)/5+10, (Screen.height)/2+45, 200, 100), "MASTER VOLUME");

			if (GUI.Button (new Rect ((Screen.width)/5+15, (Screen.height)/2+80, 100, 50), "BACK"))
			{
				scene = Menu.MAIN;
			}
			if (GUI.Button (new Rect (Screen.width-125, Screen.height-Screen.height/8-15, 75, 30), quit, ""))
			{
				Application.Quit ();
			}
		}
		
	}
}
