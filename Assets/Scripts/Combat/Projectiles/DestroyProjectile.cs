using UnityEngine;
using System.Collections;

public abstract class DestroyProjectile : MonoBehaviour {

	public float duration = 1.5f;
	public string targetTag = "Enemy";

	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == targetTag)
		{
			OnTargetCollision(coll.gameObject);
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "Wall") {
			Destroy (gameObject,0.1f);
		} 
		else {
			Destroy (gameObject, duration);
		}

	}
	protected abstract void OnTargetCollision(GameObject contact);

}
