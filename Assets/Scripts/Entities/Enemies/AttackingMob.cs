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
	private AIPath path;
	private Vector3 target;
	private MakeMap mapgen;
	
	public int initFloor;


	void Start () {

        InitializeEntity();
		AutoTarget = new AstralProjection (gameObject);

		canAttack = true;
		if (attackingg == null) 
		{
			attackingg = GameObject.FindGameObjectWithTag("Player");
			attacking = attackingg.GetComponent<Entities>();
		}

		mapgen = GameObject.FindGameObjectWithTag("MapGen").GetComponent<MakeMap>();
		initFloor = mapgen.DungeonFloor;
		ai = new MovementAI (mapgen.currentFloor ());
		path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
		ai.currentNode = path.pop ();
	}
	

	void FixedUpdate () {
		if ((attackingg.transform.position - gameObject.transform.position).magnitude <= 8.0) {
			ai.fpscounter++;

			if (ai.fpscounter > 40) {
					ai.fpscounter = 0;
					path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
					path.pop ();
					ai.currentNode = path.pop ();
			}

			if (Vector3.SqrMagnitude (this.transform.position - target) < 0.01) {
					this.rigidbody2D.velocity = new Vector2 (0, 0);
					ai.currentNode = path.pop ();
			}

			if (ai.currentNode != null) {
					target = new Vector3 (ai.currentNode.loc.x, ai.currentNode.loc.y, 0);
					this.setDirection (target - this.transform.position);
					Move ();
			}
		}else{
			eTile[,] map = mapgen.currentFloor();
			Location[] points = new Location[10];
			
			for (int i = 0; i < points.Length; i++){
				Location l;
				do{
					l = new Location(Random.Range (0,map.GetLength(0)),Random.Range (0,map.GetLength(1)));
				}while (map[l.x, l.y] != eTile.Floor);
				points[i] = l;
			}
			
			int selectedPoint = Random.Range (0,9);
			Vector3 randomTarget = new Vector3 (points[selectedPoint].x, points[selectedPoint].y, 0);
			
			if (ai.fpscounter > 40) {
				ai.fpscounter = 0;
				path = ai.getPath (gameObject.transform.position, randomTarget);
				path.pop ();
				ai.currentNode = path.pop ();
			}
			
			if (Vector3.SqrMagnitude (this.transform.position - target) < 0.01) {
				this.rigidbody2D.velocity = new Vector2 (0, 0);
				ai.currentNode = path.pop ();
			}
			
			if (ai.currentNode != null) {
				target = new Vector3 (ai.currentNode.loc.x, ai.currentNode.loc.y, 0);
				this.setDirection (target - this.transform.position);
				Move ();
		}
		
	}

		if (Vector2.Distance (rigidbody2D.transform.position, attacking.transform.position) <= distance && canAttack)
		{
			//attackEntity();
			//StartCoroutine(waitForAttack());

		}

		if (getDistance (attackingg) < 12 && GetComponent<Status>().see) {
			AutoTarget.cast(attackingg);
			//Debug.Log ("123");
			//Debug.Log(attackingg.tag);
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

	public float getDistance(GameObject go){
		return (go.transform.position - gameObject.transform.position).sqrMagnitude;
	}
}
