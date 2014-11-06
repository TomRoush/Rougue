using UnityEngine;
using System.Collections;

public class AttackingMob : Entities {

	public GameObject attackingg;
	public Entities attacking;
	public int distance;
	public int lowRangeDamage;
	public int highRangeDamage;
	public Vector3 v;

	private bool canAttack;

	void Start () {

        InitializeEntity();

		canAttack = true;
		if (attackingg == null) 
		{
			attackingg = GameObject.FindGameObjectWithTag("Player");
			attacking = attackingg.GetComponent<Entities>();
		}

		if(v == null)
		{
			v = attacking.transform.position;
		}
	}
	

	void FixedUpdate () {
        
        this.setDirection(attackingg.transform.position - this.transform.position);
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

	public GameObject getGameObj()
	{
		return attacking;
	}

	public Vector3 getV()
	{
		return enemy.transform.position;
	}
}
