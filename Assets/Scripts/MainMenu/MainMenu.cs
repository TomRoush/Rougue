using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private enum Menu {
		MAIN,
		SETTINGS,
		LOAD }
	;
	private static Menu scene = Menu.MAIN;

	GameObject menuUI;
	GameObject settingsUI;
	Slider volumeSlider;
	
	private AudioClip sheath;

	private Texture2D fade;

	VolumeManager manager;
	AudioPlayer music;
	AudioPlayer sheathPlayer;

	public float volume = 0.0f;

	private float alpha = 1.0f;

	private bool clicked = false;

	public void Start() {
		fade = Resources.Load("Artwork/Main_Menu/black") as Texture2D;

		menuUI = GameObject.Find("MenuUIGroup");
		settingsUI = GameObject.Find("SettingsUIGroup");
		volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();

		sheath = Resources.Load("Sounds/Sound_Effects/sheath") as AudioClip;
		manager = ((VolumeManager)FindObjectOfType(typeof(VolumeManager)));
		music = new AudioPlayer(gameObject, Resources.Load("Sounds/Music/Rougue Theme") as AudioClip, VolumeManager.TypeOfAudio.MUSIC);
		sheathPlayer = new AudioPlayer(gameObject, sheath, VolumeManager.TypeOfAudio.MASTER);

		music.setLoop(true);
		music.play();
	}

	public void Update() {
		if(scene == Menu.MAIN) {
			menuUI.SetActiveRecursively(true);
			settingsUI.SetActiveRecursively(false);
		} else {
			menuUI.SetActiveRecursively(false);
			settingsUI.SetActiveRecursively(true);
			volume = volumeSlider.value * 10;
			manager.setVolume(VolumeManager.TypeOfAudio.MASTER, volume);
		}
		AudioListener.volume = volume / 10;

	}

	public void onClickNewGame() {
		clicked = true;
		sheathPlayer.play();
	}

	public void onClickLoadGame() {
		clicked = true;
		sheathPlayer.play();
	}

	public void onClickSettings() {
		sheathPlayer.play();
		scene = Menu.SETTINGS;
	}

	public void onClickQuit() {
		Application.Quit();
	}

	public void onClickBack() {
		scene = Menu.MAIN;
	}

	public void OnGUI() {
//		if(scene == Menu.MAIN) {
//			if(GUI.Button(new Rect((Screen.width) / 17, (Screen.height) / 2, 200, 100), new_game, "")) {
//				clicked = true;
//				sheathPlayer.play();
//			}
//			if(GUI.Button(new Rect((Screen.width) / 17, (3 * (Screen.height)) / 4, 200, 100), settings, "")) {
//				sheathPlayer.play();
//				scene = Menu.SETTINGS;
//			}
//			if(GUI.Button(new Rect((Screen.width) / 17, (5 * (Screen.height)) / 8, 200, 100), load_game, "")) {
//				sheathPlayer.play();
//				Application.LoadLevel("Game");
//			}
//			if(GUI.Button(new Rect(Screen.width - 125, Screen.height - Screen.height / 8 - 15, 75, 30), quit, "")) {
//				Application.Quit();
//			}
//			
//		} else if(scene == Menu.SETTINGS) {
//			volume = GUI.HorizontalSlider(new Rect((Screen.width) / 5 + 15, (Screen.height) / 2 + 20, 100, 50), volume, 0.0f, 10.0f);
//
////			audio.volume = this.volume;
//			manager.setVolume(VolumeManager.TypeOfAudio.MASTER, volume);
//
//			GUI.Label(new Rect((Screen.width) / 5 + 10, (Screen.height) / 2 + 45, 200, 100), "MASTER VOLUME");
//
//			if(GUI.Button(new Rect((Screen.width) / 5 + 15, (Screen.height) / 2 + 80, 100, 50), "BACK")) {
//				scene = Menu.MAIN;
//			}
//			if(GUI.Button(new Rect(Screen.width - 125, Screen.height - Screen.height / 8 - 15, 75, 30), quit, "")) {
//				Application.Quit();
//			}
//		}

		if(clicked) {
			if(alpha >= 1) {
				Application.LoadLevel("Game");
			}
			if(alpha < 2) {
				alpha += .007f;
				alpha = Mathf.Clamp01(alpha);   
				
				Color temp = GUI.color;
				temp.a = alpha;
				GUI.color = temp;
				
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fade);
			}
		} else if(alpha > 0) {
			alpha -= .007f;
			alpha = Mathf.Clamp01(alpha);   
			
			Color temp = GUI.color;
			temp.a = alpha;
			GUI.color = temp;
			
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fade);
		}
	}
}