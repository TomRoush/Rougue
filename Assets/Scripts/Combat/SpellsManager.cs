using UnityEngine;
using System.Collections;

public class SpellsManager : MonoBehaviour {
	private Spell<GameObject>[] spells = new Spell<GameObject>[3];
	public Texture2D[] cooldownIcons;
	private Texture2D[] appliedTextures = new Texture2D[3];
	
	private float[] progress = new float[3];
	private Color overlayColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	
	// Use this for initialization
	void Start () {
		spells[0] = GetComponent<Player>().AutoTarget;
		spells[1] = GetComponent<Player>().SelfCast;
		spells[2] = GetComponent<Player>().AutoTarget2;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < spells.Length; i++) {
			progress[i] = (Time.time - spells[i].getLastCastTime()) / spells[i].getCoolDown();
			appliedTextures[i] = UpdateProgress(cooldownIcons[i], progress[i]);
		}
	}
	
	void OnGUI() {
		for(int i = 0; i < spells.Length; i++) {
			GUI.DrawTexture(new Rect(10 + 37 * i, Screen.height - 42, 32, 32), appliedTextures[i], ScaleMode.ScaleToFit, true, 1.0f);
		}
	}
	
	Texture2D UpdateProgress(Texture2D tex, float p){
		Texture2D overlay = new Texture2D(tex.width, tex.height);
		Vector2 center = new Vector2(Mathf.Ceil(overlay.width / 2), Mathf.Ceil(overlay.height / 2));
		
		for(int y = 0; y < overlay.height; y++){
			for(int x = 0; x < overlay.width; x++){
				float angle = Mathf.Atan2(x-center.x, y-center.y) * Mathf.Rad2Deg;
				
				if(angle < 0){
					angle += 360;
				}
				
				Color pixColor = tex.GetPixel(x, y);
				if(angle >= p * 360.0){
					pixColor = new Color(
						(pixColor.r * pixColor.a * (1 - overlayColor.a)) + (overlayColor.r * overlayColor.a),
						(pixColor.g * pixColor.a * (1 - overlayColor.a)) + (overlayColor.g * overlayColor.a),
						(pixColor.b * pixColor.a * (1 - overlayColor.a)) + (overlayColor.b * overlayColor.a)
					);
				}
				
				overlay.SetPixel(x, y, pixColor);
			}
		}
		overlay.Apply();
		return overlay;
	}
}
