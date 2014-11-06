using UnityEngine;
using System.Collections;

public class Mudball : Spell {
	
	//public float mudballTimer;
	public GameObject mudball;
	public GameObject closest;
	public GameObject player;
	
	// Use this for initialization
	public Mudball (GameObject pCaster) : base(pCaster) {
		manaCost = 10;
		name = "Mudball";
		coolDown = 7;
		mudball = Resources.Load ("Mudball") as GameObject;
		//player = GameObject.FindGameObjectWithTag ("Player");
		//mudballTimer = 0; 
		//parent = transform.parent;
		//fireball = (GameObject) Resources.Load ("Fireball");
		
	}
	
	// Update is called once per frame
	protected override void CastSpell (GameObject closest) {
		player = caster;
		if (!player.GetComponent<Status>().isStunned && Input.GetKey (KeyCode.C)) {
			//closest = player.GetComponent<Status>().FindClosestEnemy();
			//Debug.Log("mud");
			
			//Vector3 vpp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			
			float bulletspeed = 900f;
			//float xval = (closest.transform.position.x - player.transform.position.x) ;
			//float yval = (closest.transform.position.y - player.transform.position.y) ;
			float xval = (closest.transform.position.x - player.transform.position.x) ;
			float yval = (closest.transform.position.y - player.transform.position.y) ;
			Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f)/caster.GetComponent<Status>().getDistance(closest);
			
			float angle = Mathf.Atan2(yval, xval) * 180 / (Mathf.PI) - 90;
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(0, 0, angle);
			GameObject fball = GameObject.Instantiate (mudball, player.transform.position + 100*toward/toward.sqrMagnitude, rotation) as GameObject;
			fball.GetComponent<Rigidbody2D>().AddForce(toward);

		}
	}
}
