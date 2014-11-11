using UnityEngine;
using System.Collections;

// Attached to projectile prefabs to handle collision behavior
public class DestroyBullet : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// If collided with enemy
		if (coll.gameObject.tag == "Enemy") {
			// Destroy bullet
			Destroy (gameObject);
			// Decrease health of enemy
			coll.gameObject.GetComponent<Status>().health -= 10;
		
		// If collided with wall)
		} else if (coll.gameObject.tag == "Wall") {
			// Destroy bullet
			Destroy (gameObject);

		// Else, destroy bullet after 0.5 secs
		} else {
			Destroy (gameObject, 0.5f);
		}
	}
}
