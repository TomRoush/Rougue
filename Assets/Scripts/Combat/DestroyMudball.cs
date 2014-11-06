using UnityEngine;
using System.Collections;

public class DestroyMudball : MonoBehaviour {
	
	public GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<Status>().health-=player.GetComponent<Status>().intelligence*0.2f;
			coll.gameObject.GetComponent<Status>().getSlowed=true;
			coll.gameObject.GetComponent<Status>().slowTimer=3;
		
			Destroy (gameObject);
			
		} else if (coll.gameObject.tag == "Wall") {
			Debug.Log ("collisionwall");
			Destroy (gameObject);
		} else {
			Destroy (gameObject, 1f);
		}
	}
	
	public float getDistance(GameObject go){
		return (go.transform.position - transform.position).sqrMagnitude;
	}
}
