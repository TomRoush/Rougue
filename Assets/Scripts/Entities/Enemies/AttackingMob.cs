using UnityEngine;
using System.Collections;

public class AttackingMob : Entities {

	public GameObject attackingg;
	public Entities attacking;
	public int distance;
	public int lowRangeDamage;
	public int highRangeDamage;

	private bool canAttack;

	void Start () {

        cStat = GetComponent<Status>();
		canAttack = true;
		if (attackingg == null) 
		{
			attackingg = GameObject.FindGameObjectWithTag("Player");
			attacking = attackingg.GetComponent<Entities>();
		}
	}
	

	void FixedUpdate () {
        float dy = 0;
        float dx = 0;

		if (attacking.rigidbody2D.transform.position.y > (rigidbody2D.transform.position.y + distance)) 	
		//if (attackingg.transform.position.y > (gameObject.transform.position.y + distance)) 	
		{
            dy = 1;
		}
		
		if (attacking.rigidbody2D.transform.position.y < (rigidbody2D.transform.position.y - distance)) 
		//if (attackingg.transform.position.y < (gameObject.transform.position.y + distance))
		{
            dy = -1;
		}
		
		if (attacking.rigidbody2D.transform.position.x > (rigidbody2D.transform.position.x + distance))
		//if (attackingg.transform.position.x > (gameObject.transform.position.x + distance))
		{
            dx = 1;
		}
		
		if (attacking.rigidbody2D.transform.position.x < (rigidbody2D.transform.position.x - distance)) 
		//if (attackingg.transform.position.x < (gameObject.transform.position.x + distance))
		{
            dx = -1;
		}
        this.setDirection(new Vector3(dx,dy,0));
        Move();

		if (Vector2.Distance (rigidbody2D.transform.position, attacking.transform.position) <= distance && canAttack)
		{
			attackEntity();
			StartCoroutine(waitForAttack());

		}
	}

	public void attackEntity()
	{
		int take = Random.Range (lowRangeDamage, highRangeDamage);
		attacking.takeHealth(take);
	}

	IEnumerator waitForAttack()
	{
		canAttack = false;
		yield return new WaitForSeconds(1);
		canAttack = true;

	}
}
