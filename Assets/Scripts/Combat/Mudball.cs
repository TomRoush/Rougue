using UnityEngine;
using System.Collections;

public class Mudball : MonoBehaviour {
	
	public float mudballTimer;
	public Rigidbody2D mudball;
	public GameObject closest;
	public GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mudballTimer = 0; 
		//parent = transform.parent;
		//fireball = (GameObject) Resources.Load ("Fireball");
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!player.GetComponent<Status>().isStunned && Input.GetKey (KeyCode.C) && mudballTimer<=0 
		    && player.GetComponent<Status>().mana > 10) {
			player.GetComponent<Status>().mana -= 10;
			closest = player.GetComponent<Status>().FindClosestEnemy();
			Debug.Log("mud");
			
			//Vector3 vpp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			
			float bulletspeed = 900f;
			//float xval = (closest.transform.position.x - player.transform.position.x) ;
			//float yval = (closest.transform.position.y - player.transform.position.y) ;
			float xval = (closest.transform.position.x - player.transform.position.x) ;
			float yval = (closest.transform.position.y - player.transform.position.y) ;
			Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f)/GetComponent<Status>().getDistance(closest);
			
			Rigidbody2D fball = Instantiate (mudball, player.transform.position + 100*toward/toward.sqrMagnitude, Quaternion.identity) as Rigidbody2D;
			fball.AddForce(toward);
			mudballTimer=6f;
		}
		if (mudballTimer>0){
			mudballTimer-=Time.deltaTime;
		}
	}
}
