using UnityEngine;
using System.Collections;

public abstract class DestroyProjectile : MonoBehaviour {

	public float duration = 1.5f;
	public string targetTag = "Enemy";
	public GameObject endEffect;

	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == targetTag)
		{
			OnTargetCollision(coll.gameObject);
			if(endEffect != null) Instantiate(endEffect, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "Wall") {
			if(endEffect != null) Instantiate(endEffect, transform.position, Quaternion.identity);
			Destroy (gameObject,0.1f);
		} 
		else {
			Destroy (gameObject, duration);
			//if(endEffect != null) Instantiate(endEffect, transform.position, Quaternion.identity);
		}
	}
	protected abstract void OnTargetCollision(GameObject contact);

}
