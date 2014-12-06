using UnityEngine;
using System.Collections;

public class DestroyAstralProjection : DestroyProjectile {
	
	private int damage;
	GameObject player;
	
	public void Initialize(int pDamage)
	{
		targetTag = "Player";
		damage = pDamage;
		player = GameObject.FindGameObjectWithTag(targetTag);
	}

	private void FixedUpdate () {

		float xval = (player.transform.position.x - gameObject.transform.position.x) ;
		float yval = (player.transform.position.y - gameObject.transform.position.y) ;
		Vector3 toward = new Vector3(xval * 25, yval * 25, 1.9f)/player.GetComponent<Status>().getDistance(gameObject);
		gameObject.GetComponent<Rigidbody2D>().AddForce(toward);
	}
	
	protected override void OnTargetCollision(GameObject contact) {
		
		contact.GetComponent<Status>().MagicDamage(damage);
		//coll.gameObject.GetComponent<Status>().getSlowed=true;
		//coll.gameObject.GetComponent<Status>().slowTimer=3;
		//GameObject slowEffect = Resources.Load ("Slow") as GameObject; 
		//slowEffect.GetComponent<TimedEffect>().target = contact;
		//GameObject.Instantiate(slowEffect, contact.transform.position, Quaternion.identity);
		
		
		
	}
	
}
