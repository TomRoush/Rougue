using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackingMob : Entities {

	public GameObject attackingg;
	public Entities attacking;
	public int distance;
	public int lowRangeDamage;
	public int highRangeDamage;

	private bool canAttack;

	private MovementAI ai;
	private List<Node> path;
	private Vector3 target;

	void Start () {

        InitializeEntity();

		canAttack = true;
		if (attackingg == null) 
		{
			attackingg = GameObject.FindGameObjectWithTag("Player");
			attacking = attackingg.GetComponent<Entities>();
		}

		MakeMap mapgen = GameObject.FindGameObjectWithTag("MapGen").GetComponent<MakeMap>();
		ai = new MovementAI (mapgen.currentFloor ());
		path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
	}
	

	void FixedUpdate () {
		if (ai.fpscounter > 40) {
			ai.nodecounter = 1;
			path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
			ai.fpscounter = 0;
		}
		ai.fpscounter++;
		if (Vector3.SqrMagnitude(this.transform.position - target) < 0.01) {
			this.rigidbody2D.velocity = new Vector2(0, 0);
			if (ai.nodecounter < path.Count - 1)
				ai.nodecounter++;
		}
		target = new Vector3 (path[ai.nodecounter].loc.x, path[ai.nodecounter].loc.y, 0);
		this.setDirection(target - this.transform.position);
		Move();

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
