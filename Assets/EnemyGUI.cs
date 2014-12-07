using UnityEngine;
using System.Collections;

public class EnemyGUI : MonoBehaviour {
	private Vector2 pos = new Vector2(120, 20);
	private Vector2 size = new Vector2(100, 20);
	private Texture2D empty;
	private Texture2D full;
	private GUIStyle current = null;
	public Texture2D healthbarTexture;
	float healthFraction;

	void OnGUI(){
		/*GUI.color = Color.red;
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), empty);
	
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, gameObject.GetComponent<Status> ().getPercentHealth(), size.y));//gameObject.GetComponent<Status> ().health?
		GUI.Box(new Rect(0,0, size.x, size.y), full, current);
		GUI.EndGroup();
		GUI.EndGroup();*/
		DrawHealthbar();
	}
	void DrawHealthbar () {
		healthFraction = (float)(gameObject.GetComponent<Status>().getPercentHealth()/100f);//50%
		var positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
		positionOnScreen.y = Screen.height - positionOnScreen.y;
		//positionOnScreen.x *= Screen.width;
		//positionOnScreen.y *= Screen.height;
		var scaleFactor = 1 / 20f;
		var healthyWidth = healthbarTexture.width * scaleFactor;//pixels.
		var healthbarHeight = healthbarTexture.height * scaleFactor;
		var currentWidth = healthFraction * healthyWidth;
		var aboveHeadOffset = 10;//between transform.position transformed, and center of healthbar.
		GUI.DrawTexture(new Rect(positionOnScreen.x - healthyWidth / 2, positionOnScreen.y - aboveHeadOffset - healthbarHeight / 2, currentWidth, healthbarHeight), healthbarTexture, ScaleMode.ScaleAndCrop);
		//GUI.DrawTexture(new Rect(20, 20, 500, 500), healthbarTexture);
	}
}
