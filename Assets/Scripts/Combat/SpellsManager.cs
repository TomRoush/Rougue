using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellsManager : MonoBehaviour {
	private SpellInfo[] spells;
	
	private Color overlayColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private PlayerGUIBars playerBars;
    private int boxH;
    private int boxW;
    private int boxX;
    private int boxY;
    public float boxGp;
	
	public struct SpellInfo {
		public Spell<GameObject> spell;
		public Texture2D appliedTexture;
		public float progress;
	}
	
	// Use this for initialization
	void Start () {
        playerBars = GetComponent<PlayerGUIBars>();
		List<SpellInfo> infoList = new List<SpellInfo>();
		if(GetComponent<Player>().AutoTarget != null) {
			SpellInfo info = new SpellInfo();
			info.spell = GetComponent<Player>().AutoTarget;
			infoList.Add(info);
		}
		if(GetComponent<Player>().SelfCast != null) {
			SpellInfo info = new SpellInfo();
			info.spell = GetComponent<Player>().SelfCast;
			infoList.Add(info);
		}
		if(GetComponent<Player>().AutoTarget2 != null) {
			SpellInfo info = new SpellInfo();
			info.spell = GetComponent<Player>().AutoTarget2;
			infoList.Add(info);
		}
		if(GetComponent<Player>().SelfCast2 != null) {
			SpellInfo info = new SpellInfo();
			info.spell = GetComponent<Player>().SelfCast2;
			infoList.Add(info);
		}
		spells = infoList.ToArray();

		boxH = playerBars.getBH();
		boxW = playerBars.getBW();
		boxX = playerBars.getBX();
		boxY = playerBars.getBY();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < spells.Length; i++) {
			spells[i].progress = (Time.time - spells[i].spell.getLastCastTime()) / spells[i].spell.getCoolDown();
			spells[i].appliedTexture = UpdateProgress(spells[i].spell.getCooldownIcon(), spells[i].progress);
		}
	}
	
	void OnGUI() {
        GUI.depth = -10;
        int size = (int)(Mathf.Min(((boxW) / 4), (boxH)));
		for(int i = 0; i < spells.Length; i++) {
			//int size = (int)(40.0 * (Screen.height / 520.0)); // Relative to my 520 base height
			GUI.DrawTexture(new Rect(boxX + i * size + i * 5 - size / 4, boxY - size / 2, size, size), spells[i].appliedTexture, ScaleMode.ScaleToFit, true, 1.0f);
			
			// Clean up texture references
			Texture2D[] textures = FindObjectsOfType(typeof(Texture2D)) as Texture2D[];
			foreach (Texture2D t in textures) {
				Destroy(t);
			}
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
