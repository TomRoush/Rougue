using UnityEngine;
using System.Collections;

public class RandomizeSprite : MonoBehaviour {
	public Sprite[] sprites; // An array of the sprites to pick from
	
	// Just grab the sprite renderer
	void Start () {
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
	}
}
