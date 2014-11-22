using UnityEngine;
using System.Collections;

public class DestroyFireball : MonoBehaviour {

	public GameObject player;
	public float aoe = 5f;	
	public LayerMask enemiesWalls;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (coll.gameObject.tag == "Enemy") {
			foreach (GameObject go in enemies) {
				if (getDistance(go)<=aoe){
					var heading = go.transform.position - transform.position;
					var distance2 = heading.magnitude;
					var direction = heading/distance2;
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, aoe, enemiesWalls);
					if (hit.collider.tag == "Enemy"){
						go.gameObject.GetComponent<Status>().health-=player.GetComponent<Status>().intelligence*2;
					}
				}
			}
			Debug.Log ("FBcollisionenemy");
			Destroy (gameObject);
			
		} else if (coll.gameObject.tag == "Wall") {
			Debug.Log ("collisionwall");
			Destroy (gameObject);
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
