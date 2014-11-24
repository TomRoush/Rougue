using UnityEngine;
using System.Collections;

public class DestroyFireball : DestroyProjectile {


	public float aoe = 5f;	
	public LayerMask enemiesWalls;
	public int damage;

	public void Initialize(int pDamage)
	{
		damage = pDamage;
	}
	
	protected override void OnTargetCollision(GameObject contact) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject go in enemies) {
				if (getDistance(go)<=aoe){
					var heading = go.transform.position - transform.position;
					var distance2 = heading.magnitude;
					var direction = heading/distance2;
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, aoe, enemiesWalls);
					if (hit.collider.tag == "Enemy"){
						go.gameObject.GetComponent<Status>().health-= damage;
					}
				}
			}
	}


	public float getDistance(GameObject go){
		return (go.transform.position - transform.position).sqrMagnitude;
	}
}
