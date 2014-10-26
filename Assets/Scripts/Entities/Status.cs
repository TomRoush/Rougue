using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	//public Hashtable<string, float> attributes = new Hashtable<string, float>();
	GameObject[] enemies; //= new GameObject[200];
	static GameObject player;
	[HideInInspector]
	public int level = 5;
	[HideInInspector]
	public bool levelUp = false;
	public int upgradePoints;
	[HideInInspector]
	public int floor = 1;

	public float speed;
	public float speedx;//speed-buff or slow-debuff
	public float agility;//turn speed
	public float agilityx;

	public float maxHealth;
	[HideInInspector]
	public float health;
	public float healthRegen;
	
	public float maxMana;
	[HideInInspector]
	public float mana;
	public float manaRegen;
	
	[HideInInspector]
	public float rage = 0.0f;
	public float rageDecay;
	[HideInInspector]
	public float rageTimer = 0.0f;
	[HideInInspector]
	public bool isRaged = false;
	
	public float attackSpeed;
	[HideInInspector]
	public float attackTimer = 0.0f;
	
	public float damagex;
	public float strength;//type 1
	public float defense;//type 1 only defends against type1
	public float intelligence;
	public float resistence;

	public float range1;
	
	[HideInInspector]
	public bool isStunned = false;
	[HideInInspector]
	public bool isSlowed = false;

	public float exp1;
	public float money1;
	public float money2;
	
	// Use this for initialization
	void Start () {
		if (gameObject.tag == "Enemy") {
			level = Random.Range (floor, floor+10);
		}

		maxHealth = 100f*(Mathf.Pow(1.05f, level-1));
		
		health = maxHealth;
		mana = maxMana;

		strength = 20f+level;//type 1
		//	damage1 = 25f; 
		intelligence = 50f+level;

		//enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		player = GameObject.FindGameObjectWithTag ("Player");
	}


	
	// Update is called once per frame
	void Update () {

		if (health <= 0 && gameObject.tag=="Enemy") {
			player.GetComponent<Status>().money1+=10+level;//don't think the (Clone) part is needed, but I put anyways; could also maybe find by tag "Player"
			player.GetComponent<Status>().exp1+=10+level;
			Destroy (gameObject);
		}

		if (exp1>100+10*level){
			exp1-=100+10*level;
			level++;
			maxHealth *= 1.05f;
			maxMana *= 1.05f;
			strength++;
			intelligence++;
			upgradePoints++;
		}
		
		if (health>maxHealth){
			health=maxHealth;
		}else if (health<maxHealth){
			health += Time.deltaTime * healthRegen;
		}
		
		if(mana > maxMana) {
			mana = maxMana;
		} else if(mana < maxMana){
			mana += Time.deltaTime * manaRegen;
		}
		
		if (rage < 0) {
			rage=0;
		}else if (rage>=100&&gameObject.tag == "Player" && !isRaged){//rage on
			speedx+=1f;//speed does not work
			damagex+=10f;//damage works though
			attackSpeed+=0.5f;
			rageTimer = 6f; 
			isRaged=true;
		}else if (rage > 0) {
			rage -= Time.deltaTime * rageDecay;
		}
		if (rageTimer>0){
			rageTimer -= Time.deltaTime;
		}else if (isRaged && rageTimer<=0){//rage off
			speedx-=1f;
			damagex-=10f;
			attackSpeed-=0.5f;
			isRaged=false;
			rageTimer=0;
			rage = 0;
		}
		
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}

		if (isSlowed) {
			isSlowed=false;
		}

		autoMelee ();
	}
	
	//void OnCollisionStay2D (Collision2D collider){
	void autoMelee(){//not necessarily melee range (uses range1), but this uses strength
		GameObject closest = FindClosestEnemy();
		if (closest != null){
			if (gameObject.tag == "Player" && 
		    	getDistance(closest) < range1 &&
		    	//Mathf.Sqrt((enemies[i].transform.position.x-gameObject.transform.position.x) * 
		        //   (enemies[i].transform.position.x-gameObject.transform.position.x) + 
		        //   (enemies[i].transform.position.y-gameObject.transform.position.y) *
		        //   (enemies[i].transform.position.y-gameObject.transform.position.y)) < range1 
				attackTimer <= 0) 
			{
			//Debug.Log ("Yes1");
				closest.gameObject.GetComponent<Status> ().health -= strength * damagex
					* closest.gameObject.GetComponent<Status> ().defense;

				if (!closest.gameObject.GetComponent<Status> ().isRaged) {
					closest.gameObject.GetComponent<Status> ().rage += strength * damagex
					* closest.gameObject.GetComponent<Status> ().defense * 2;//gain twice rage as loss in hp
				}
				//money1+=0.1f;
				attackTimer = 1 / attackSpeed;
			}
		}
		if (gameObject.tag=="Enemy" && 
		   		getDistance(player) < range1 && 
			    //Mathf.Sqrt((player.transform.position.x-gameObject.transform.position.x) * 
		        // 			(player.transform.position.x-gameObject.transform.position.x) + 
			    //      		(player.transform.position.y-gameObject.transform.position.y) *
		        //  			(player.transform.position.y-gameObject.transform.position.y)) < range1 
			  	attackTimer <= 0) 
			{
				//Debug.Log ("Yes2");
				player.gameObject.GetComponent<Status> ().health -= strength * damagex
					* player.gameObject.GetComponent<Status> ().defense;
					
				if (!player.gameObject.GetComponent<Status> ().isRaged){
					player.gameObject.GetComponent<Status> ().rage += strength * damagex
						* player.gameObject.GetComponent<Status> ().defense * 2;//gain twice rage as loss in hp
				}
				player.gameObject.GetComponent<Player>().blood.Play();
				attackTimer = 1/attackSpeed;
		}
	}

	GameObject FindClosestEnemy() {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject closest = null;
		if (gameObject.tag == "Player"){
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in enemies) {
				float curDistance = (go.transform.position - position).sqrMagnitude;
				//Vector3 diff = go.transform.position - position;
				//float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
		}
		if (closest != null) {
			return closest;
		} else {
			return null;
		}
	}
	float getDistance(GameObject go){
		return (go.transform.position - transform.position).sqrMagnitude;
	}

//	public void buff(float duration, float startTime, float repeatTime){
//		if (repeatTime>0){
//			duration-=Time.deltaTime;
//			if (duration==0){
//				return;
//			}
//		}
//
//	}

	public float getSpeedx(){//not sure if needed anymore
		return speedx;
	}
}
