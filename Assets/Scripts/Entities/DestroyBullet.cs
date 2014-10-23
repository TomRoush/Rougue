using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("collisionenemy");
			Destroy (gameObject);

			coll.gameObject.GetComponent<AttackingMob>().takeHealth(30);
		} else if (coll.gameObject.tag == "Wall") {
			Debug.Log ("collisionwall");
		} else {
			Destroy (gameObject, 0.5f);
		}
	}
}
