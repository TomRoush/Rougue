using UnityEngine;
using System.Collections;

// GUI CONTROLLER for PLAYER

public class PlayerGUI : MonoBehaviour {

	private Player player;

	public bool paused;
	public bool alive;

	// HEALTH/MANA BARS
	private Vector2 pos = new Vector2(20,40);
	private Vector2 size = new Vector2(100,20);

	private Texture2D emptyTex;
	private Texture2D fullTex;
	private Texture2D background;
	private Texture2D healthBarTexture;
	private Texture2D manaBarTexture;
	private Texture2D rageBarTexture;
	private Texture2D outlineBar;
	float healthFraction;

	// death messages
	private static Texture2D[] messages;

	private Texture2D decimated;
	private Texture2D devoured;

	private int message;
	// end of death messages

	private GUIStyle currentStyle = null;

	private float alpha = 1.0f;

	public PlayerGUI(Player player)
	{
		this.player = player;
		this.paused = player.paused;
		this.alive = player.alive;

		decimated = Resources.Load ("Artwork/InGame/death_messages/decimated") as Texture2D;
		devoured = Resources.Load ("Artwork/InGame/death_messages/devoured") as Texture2D;

		messages = new Texture2D[2];
		messages[0] = decimated;
		messages[1] = devoured;

		//generates index of message randomly
		message = (int) Random.Range (0f, 2f);

		background = Resources.Load ("Artwork/Main_Menu/black") as Texture2D;
	}

	public void onGUI()
	{
		if (paused) 
		{
			alpha = Mathf.Clamp01(.65f);
			
			Color temp = GUI.color;
			temp.a = alpha;
			GUI.color = temp;

			GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height), background);

			alpha = Mathf.Clamp01(1f);
			
			temp = GUI.color;
			temp.a = alpha;
			GUI.color = temp;

			if(alive) {
				if(GUI.Button (new Rect(((Screen.width)/2)-50, ((Screen.height)/2)-50, 100, 50), "CONTINUE")) 
				{
					this.paused = false;
					player.paused = false;
					player.UpdateGameState();
				}
				if(GUI.Button (new Rect(((Screen.width)/2)-50, ((Screen.height)/2)+50, 100, 50), "SAVE & QUIT")) 
				{
					Application.LoadLevel("MainMenu");
				}
			}
		}	
		if (player.gameObject.GetComponent<Status> ().getHealth()<=0.0f){

			alpha = Mathf.Clamp01(.65f);
			
			Color temp = GUI.color;
			temp.a = alpha;
			GUI.color = temp;
			
			GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height), background);
			
			alpha = Mathf.Clamp01(1f);
			
			temp = GUI.color;
			temp.a = alpha;
			GUI.color = temp;

			GUI.Label (new Rect(Screen.width/2-88, Screen.height/2-60, 175, 60), messages[message]);
			if(GUI.Button (new Rect(Screen.width/2-50, Screen.height/2 + 50, 100, 50), "MAIN MENU")) 
			{
				Application.LoadLevel("MainMenu");
			}
		}
	}
}
