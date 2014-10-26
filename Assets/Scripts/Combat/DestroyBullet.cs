using UnityEngine;
using System.Collections;

// Attached to projectile prefabs to handle collision behavior
public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		// If collided with enemy
		if (coll.gameObject.tag == "Enemy") {
			// Destroy bullet
			Destroy (gameObject);
			// Decrease health of enemy
			coll.gameObject.GetComponent<AttackingMob>().takeHealth(30);
		
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
