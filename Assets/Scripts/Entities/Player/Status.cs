using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	//public Hashtable<string, float> attributes = new Hashtable<string, float>();
	GameObject[] enemies; //= new GameObject[200];
	static GameObject player;

	public int level;
	public bool levelUp;
	public int upgradePoints;
	public int floor;

	public float speed;
	public float speedx;//speed-buff or slow-debuff
	public float agility;//turn speed
	public float agilityx;

	public float maxHealth;
	public float health;
	public float healthRegen;
	
	public float rage;
	public float rageDecay;
	public float rageTimer;
	public bool isRaged;
	
	public float attackSpeed;
	public float attackTimer;
	
	public float damagex;
	public float strength;//type 1
	public float defense;//type 1
	public float intelligence;
	public float resistence;

	public float range1;
	
	public bool isStunned;
	public bool isSlowed;

	public float exp1;
	public float money1;
	public float money2;
	
	// Use this for initialization
	void Start () {
		level = 5;
		if (gameObject.tag == "Enemy") {
			level = Random.Range (floor, floor+10);
		}
		levelUp = false;
		floor = 1;

		speed = 5f;
		speedx = 1f;//speed-buff or slow-debuff
		agility = 180f;
		agilityx = 1f;

		maxHealth = 100f*(Mathf.Pow(1.05f, level-1));
		health = maxHealth;
		healthRegen = 1f;
		if (gameObject.tag == "Player") {
			healthRegen=2f;
		}
		damagex = 1f;
		strength = 20f+level;//type 1
		//if (gameObject.tag == "Player") {
		//	damage1 = 25f; 
		//}
		defense = 0.5f;//type 1 only defends against type1
		intelligence = 50f+level;
		resistence = 0.5f;

		range1 = 12;
		if (gameObject.tag == "Enemy") {
			range1 = 6;
		}
		
		rage = 0f;
		rageDecay = 3;
		rageTimer = 0;
		isRaged = false;
		
		attackSpeed = 1f;
		attackTimer = 0;
		
		isStunned = false;

		exp1 = 0f;
		money1 = 0f;
		money2 = 0f;

		//enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		player = GameObject.FindGameObjectWithTag ("Player");
	}


	
	// Update is called once per frame
	void Update () {

		if (health <= 0 && gameObject.tag=="Enemy") {
			GameObject.Find ("Mage WithCam(Clone)").GetComponent<Status>().money1+=10+level;//don't think the (Clone) part is needed, but I put anyways; could also maybe find by tag "Player"
			GameObject.Find ("Mage WithCam(Clone)").GetComponent<Status>().exp1+=10+level;
			Destroy (gameObject);
		}

		if (exp1>100+10*level){
			exp1-=100+10*level;
			level++;
			maxHealth+=maxHealth/0.05f;
			strength++;
			intelligence++;
			upgradePoints++;
		}
		
		if (health>maxHealth){
			health=maxHealth;
		}else if (health<maxHealth){
			health += Time.deltaTime * healthRegen;
		}
		
		if (rage < 0) {
			rage=0;
		}else if (rage>=100&&gameObject.tag == "Player"){//rage on
			speedx+=1f;//speed does not work
			damagex+=10f;//damage works though
			attackSpeed+=0.5f;
			rageTimer = 6f; 
			isRaged=true;
			rage=0;
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
			Debug.Log ("Yes1");
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
				Debug.Log ("Yes2");
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
