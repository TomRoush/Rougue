using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackingMob : Entities {

	public GameObject attackingg;
	public Entities attacking;
	public int distance;
	public int lowRangeDamage;
	public int highRangeDamage;
	public LayerMask Wall;

	private bool canAttack;
	private bool inRange;
	private bool visible;

	private MovementAI ai;
	private AIPath path;
	private Vector3 target;
	private Vector3 direction;
	private MakeMap mapgen;

	public string name;
	
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
		direction = attackingg.transform.position - gameObject.transform.position;
		if (direction.magnitude <= 8.0f) {
			inRange = true;    
		} else {
			inRange = false;
		}

		RaycastHit2D hit = Physics2D.Raycast (attackingg.transform.position, direction, 8.0f, Wall);
		
		if (hit.collider != null) {
			visible = false;
		} else {
			visible = true;
		}

		Debug.Log (visible.ToString ());
		if (visible && inRange) {
			this.setDirection (direction);
			Move ();

		}else if (!visible && inRange){
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

		if (name.Equals("Ghost") && getDistance (attackingg) < 60 && GetComponent<Status>().see) {
		//if (getDistance (attackingg) < 60 && GetComponent<Status>().see) {
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
