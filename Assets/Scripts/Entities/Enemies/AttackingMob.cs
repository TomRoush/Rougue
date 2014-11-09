using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackingMob : Entities {

	public static int mobcount;
	public int updaterate;
	public int mobnum;

	public GameObject attackingg;
	public Entities attacking;
	public int distance;
	public int lowRangeDamage;
	public int highRangeDamage;

	private bool canAttack;

	private MovementAI ai;
	private AIPath path;
	private Vector3 target;
	private MakeMap mapgen;

	// TODO: Check/reset mobcount when changing floors
	void Start () {
		updaterate = 50;
		mobnum = mobcount;
		mobcount++;

        InitializeEntity();

		canAttack = true;
		if (attackingg == null) 
		{
			attackingg = GameObject.FindGameObjectWithTag("Player");
			attacking = attackingg.GetComponent<Entities>();
		}

		mapgen = GameObject.FindGameObjectWithTag("MapGen").GetComponent<MakeMap>();
		ai = new MovementAI (mapgen.currentFloor ());
		path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
		ai.currentNode = path.pop ();
		ai.fpscounter = (updaterate / mapgen.numEnemies) * mobnum;
	}
	

	void FixedUpdate () {
		ai.fpscounter++;

		if (ai.fpscounter > updaterate) {
			ai.fpscounter = 0;
			path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
			path.pop();
			ai.currentNode = path.pop();
		}

		if (Vector3.SqrMagnitude(this.transform.position - target) < 0.01) {
			this.rigidbody2D.velocity = new Vector2(0, 0);
			ai.currentNode = path.pop ();
		}

		if (ai.currentNode != null) {
			target = new Vector3 (ai.currentNode.loc.x, ai.currentNode.loc.y, 0);
			this.setDirection(target - this.transform.position);
			Move();
		}

		if (Vector2.Distance (rigidbody2D.transform.position, attacking.transform.position) <= distance && canAttack)
		{
			//attackEntity();
			//StartCoroutine(waitForAttack());

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
