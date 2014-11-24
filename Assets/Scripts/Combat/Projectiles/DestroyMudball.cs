using UnityEngine;
using System.Collections;

public class DestroyMudball : DestroyProjectile {

	private int damage;

	public void Initialize(int pDamage)
	{
		damage = pDamage;
	}

	protected override void OnTargetCollision(GameObject contact) {
	
		contact.GetComponent<Status>().MagicDamage(damage);
			//coll.gameObject.GetComponent<Status>().getSlowed=true;
			//coll.gameObject.GetComponent<Status>().slowTimer=3;
			GameObject slowEffect = Resources.Load ("Slow") as GameObject; 
			slowEffect.GetComponent<TimedEffect>().target = contact;
			GameObject.Instantiate(slowEffect, contact.transform.position, Quaternion.identity);

			
	
	}

}
