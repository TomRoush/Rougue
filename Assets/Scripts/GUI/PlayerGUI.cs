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
	private GUIStyle currentStyle = null;

	public PlayerGUI(Player player)
	{
		this.player = player;
		this.paused = player.paused;
		this.alive = player.alive;

		background = Resources.Load ("Artwork/Main_Menu/black") as Texture2D;
	}

	public void onGUI()
	{
		if (paused) 
		{
			GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height), background);
			if(alive) {
				if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)-50, 100, 50), "CONTINUE")) 
				{
					player.paused = false;
					player.UpdateGameState();
				}
				if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)+50, 100, 50), "SAVE & QUIT")) 
				{
					Application.LoadLevel("MainMenu");
				}
			} else {
				GUI.Label (new Rect(Screen.width/2, Screen.height/2, 100, 100), "YOU HAVE DIED");
				if(GUI.Button (new Rect(Screen.width/2, Screen.height/2 + 100, 100, 50), "MAIN MENU")) 
				{
					Application.LoadLevel("MainMenu");
				}
			}
		}
		
		// draw the health bar
		//draw the background:
		if(player.gameObject.GetComponent<Status> ().health > 50.0f) {
			InitStyles (Color.green);
		} else {
			InitStyles(Color.red);
		}
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, player.gameObject.GetComponent<Status> ().health, size.y));//gameObject.GetComponent<Status> ().health?
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup();
		
		// draw the mana bar
		//draw the background:
		InitStyles (Color.blue);
		GUI.BeginGroup(new Rect(pos.x, pos.y + size.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, player.gameObject.GetComponent<Status> ().mana, size.y));//gameObject.GetComponent<Status> ().health?
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup();
		
		// draw the stamina (rage) bar
		//draw the background:
		InitStyles (Color.magenta);
		GUI.BeginGroup(new Rect(pos.x, pos.y + 2 * size.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, player.gameObject.GetComponent<Status> ().rage, size.y));//gameObject.GetComponent<Status> ().health?
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup();
		
		if (player.gameObject.GetComponent<Status> ().health<=0.0f){
			GUI.Box (new Rect(Screen.width/2,Screen.height/2,100,50),"You died");
		}
	}

	private void InitStyles(Color c)
	{
		//if (currentStyle == null) {
		currentStyle = new GUIStyle (GUI.skin.box);
		currentStyle.normal.background = MakeTex (2, 2, c);
		//}
	}
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

}
