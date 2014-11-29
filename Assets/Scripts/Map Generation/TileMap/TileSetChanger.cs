using UnityEngine;
using System.Collections;

public class TileSetChanger : MonoBehaviour {
	private SpriteRenderer renderer;

	[System.Serializable]
	public struct SpriteSet {
		public TileSet set;
		public Sprite tile;
	}
	
	public SpriteSet[] sprites;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		if(renderer == null) Debug.Log("null");
		setTile();
	}
	
	// Switches the tile based on the tileset received
	// If the selected one can't be found, it defaults to the first 
	public void setTile() {
		//if(renderer == null) renderer = gameObject.GetComponent<SpriteRenderer>();
		if(renderer == null) return;
		foreach(SpriteSet tile in sprites) {
			if(tile.set == GameObject.FindGameObjectWithTag("MapGen").GetComponent<MakeMap>().set) {
				renderer.sprite = tile.tile;
				return;
			}
		}
		if(sprites != null) {
			renderer.sprite = sprites[0].tile;
			Debug.Log("Unable to find SpriteSet");
			return;
		}
		Debug.Log("Missing sprite!");
	}
}
