using UnityEngine;
using System.Collections;

public class DestroyFireball : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (coll.gameObject.tag == "Enemy") {
			foreach (GameObject go in enemies) {
				if (getDistance(go)<=3){
					go.gameObject.GetComponent<Status>().health-=player.GetComponent<Status>().intelligence*3;
				}
			}
			Debug.Log ("FBcollisionenemy");
			Destroy (gameObject);
			
		} else if (coll.gameObject.tag == "Wall") {
			Debug.Log ("collisionwall");
		} else if (gameObject.tag == "Fireball") {
			Destroy (gameObject, 0.8f);//0.5f
		} else if (gameObject.tag == "Mine"){
			Destroy (gameObject, 20f);
		} else {
			Destroy (gameObject, 1f);
		}
	}

	public float getDistance(GameObject go){
		return (go.transform.position - transform.position).sqrMagnitude;
	}
}
