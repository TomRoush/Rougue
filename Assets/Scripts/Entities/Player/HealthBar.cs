using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public float barDisplay=1.0f; //current progress
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	
	void OnGUI() {
		//draw the background:
		InitStyles ();
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup();

		if (GUI.Button (new Rect(100, 10, 100, 30), "Take Damage")){
			barDisplay = barDisplay-0.05f;
		}
		if (barDisplay<=0.0f){
			GUI.Box (new Rect(Screen.width/2,Screen.height/2,100,50),"You died");
		}

	}

	
	private void InitStyles()
	{
		if (currentStyle == null) {
			currentStyle = new GUIStyle (GUI.skin.box);
			currentStyle.normal.background = MakeTex (2, 2, new Color (0f, 1f, 0f, 1f));
		}
		if (barDisplay < 0.5f) {
			currentStyle.normal.background = MakeTex (2, 2, new Color (1f, 0f, 0f, 1f));
		}
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