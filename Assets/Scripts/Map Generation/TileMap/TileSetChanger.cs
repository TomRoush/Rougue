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
	void Awake () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Switches the tile based on the tileset received
	// If the selected one can't be found, it defaults to the first 
	public void setTile(TileSet set) {
		if(renderer == null) return;
		foreach(SpriteSet tile in sprites) {
			if(tile.set == set) {
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
