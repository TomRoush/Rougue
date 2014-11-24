using UnityEngine;
using System.Collections;

// Attached to projectile prefabs to handle collision behavior
public class DestroyBullet : DestroyProjectile {

	
	private int damage;
	// Use this for initialization
	public void Initialize(int pDamage)
	{
		damage = pDamage;
	}
	
	protected override void OnTargetCollision(GameObject contact) {
		contact.GetComponent<Status>().MagicDamage(damage);
	}



}
