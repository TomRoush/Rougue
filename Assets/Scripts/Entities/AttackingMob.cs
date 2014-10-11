using UnityEngine;
using System.Collections;

public class AttackingMob : Entities {

	public GameObject attackingg;
	public Entities attacking;
	public int distance;

	private bool canAttack;

	void Start () {
		canAttack = true;
		if (attackingg == null) 
		{
			attackingg = GameObject.FindGameObjectWithTag("Player");
			attacking = attackingg.GetComponent<Entities>();
		}
	}
	

	void Update () {

		if (attacking.rigidbody2D.transform.position.y > (rigidbody2D.transform.position.y + distance)) 	
		{
			rigidbody2D.transform.position += Vector3.up * speed * Time.deltaTime;
		}
		
		if (attacking.rigidbody2D.transform.position.y < (rigidbody2D.transform.position.y - distance)) 	
		{
			rigidbody2D.transform.position += Vector3.down * speed * Time.deltaTime;
		}
		
		if (attacking.rigidbody2D.transform.position.x > (rigidbody2D.transform.position.x + distance)) 	
		{
			rigidbody2D.transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		if (attacking.rigidbody2D.transform.position.x < (rigidbody2D.transform.position.x - distance)) 	
		{
			rigidbody2D.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Vector2.Distance (rigidbody2D.transform.position, attacking.transform.position) <= distance && canAttack)
		{
			attackEntity();
			StartCoroutine(waitForAttack());

		}
	}

	public void Die()
	{

	}
	public void attackEntity()
	{
		int take = Random.Range (25, 75);
		attacking.takeHealth(take);
	}

	IEnumerator waitForAttack()
	{
		canAttack = false;
		yield return new WaitForSeconds(1);
		canAttack = true;

	}
}
