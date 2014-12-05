using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	//public Hashtable<string, float> attributes = new Hashtable<string, float>();
	private GameObject[] enemies; //= new GameObject[200];
	static GameObject player;
	public static GameObject closest;

	public LayerMask enemiesWalls;
	public LayerMask playerWalls;

	public int level = 5;
	public bool levelUp = false;
	public int upgradePoints;
	public int floor = 1;

    public int baseMaxHealth;
    public int baseMaxMana;
    public float baseManaRegen;
    public float baseHealthRegen;
    public float baseSpeed;
    public int baseAgility;
    public int baseStrength;
    public int baseIntelligence;
    public int baseAttackDamage;





	private float speed;
	private float speedx;//speed-buff or slow-debuff
	public float agilityx;

	[HideInInspector]
    public int agility;
    public int strength;
    public int intelligence;
    private int attackDamage;

	[HideInInspector]
    public float maxHealth;
	public float health;
	public float healthRegen;
	
	[HideInInspector]
    public float maxMana;
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
	public float defense;//type 1 only defends against type1
	public float resistence;

	public float range1;
	
	//[HideInInspector]
	public bool isStunned = false;
	public bool getSlowed = false;
	public bool isSlowed = false;
	public float slowTimer=0;

	public float exp1;
	public float money1;
	public float money2;

	public bool see=false;
	public GameObject sword;
	
	// Use this for initialization
	void Start () {
		if (gameObject.tag == "Enemy") {
			level = Random.Range (floor, floor+10);
		}
        speed = baseSpeed;
        speedx = 1;

        strength = baseStrength;
        agility = baseAgility;
        intelligence = baseIntelligence;
        attackDamage = baseAttackDamage;



        refreshStats();
		//maxHealth = 100f*(Mathf.Pow(1.05f, level-1));
		
		health = maxHealth;
		mana = maxMana;

		//strength = 25+level;//type 1
		//	damage1 = 25f; 
		//intelligence = 50+level;

		//enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		player = GameObject.FindGameObjectWithTag ("Player");
	}


    public void refreshStats() //Terrible, terrible things will happen if this is called while debuffed/buffed.
    {
        maxHealth = baseMaxHealth + 10*strength;
        maxMana = baseMaxMana + 10*intelligence;
        manaRegen = baseManaRegen + intelligence * 0.1f;
        healthRegen = baseHealthRegen + strength * 0.1f;
    }
	
	// Update is called once per frame
	void Update () {

		if (health <= 0 && gameObject.tag=="Enemy") {
        if(player == null)
            Debug.Log("NUll player???");
			player.GetComponent<Status>().money1+=10+level;//don't think the (Clone) part is needed, but I put anyways; could also maybe find by tag "Player"
			player.GetComponent<Status>().exp1+=10+level;
			Destroy (gameObject);
		}

		if (exp1>100+10*level){
			exp1-=100+10*level;
			level++;
			strength += 2;
			intelligence += 2;
            agility += 2;
			upgradePoints++;
		}
		
		if (health>maxHealth){
			health=maxHealth;
		}else if (health<maxHealth){
			health += Time.deltaTime * healthRegen*maxHealth/100;
		}
		
		if(mana > maxMana) {
			mana = maxMana;
		} else if(mana < maxMana){
			mana += Time.deltaTime * manaRegen;
		}
		
		if (rage < 0) {
			rage=0;
		}else if (rage>=100&&gameObject.tag == "Player" && !isRaged){//rage on
			GameObject rageEffect = Resources.Load ("Rage") as GameObject; 
			rageEffect.GetComponent<TimedEffect>().target = gameObject;
			GameObject.Instantiate(rageEffect, player.transform.position, Quaternion.identity);
			//speedx+=1f;
			//damagex+=10f;
			//attackSpeed+=0.5f;
			//rageTimer = 6f; 
			//isRaged=true;
		}else if (rage > 0) {
			rage -= Time.deltaTime * rageDecay;
		}
		if (rageTimer>0){
			rageTimer -= Time.deltaTime;
		}else if (isRaged && rageTimer<=0){//rage off
			//speedx-=1f;
			//damagex-=10f;
			//attackSpeed-=0.5f;
			//isRaged=false;
			rageTimer=0;
			rage = 0;
		}
		
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.T)){
			getSlowed=true;
		}

		if (getSlowed && !isSlowed) {
			speedx-=0.8f;
			isSlowed=true;
			getSlowed=false;
			slowTimer=3;//default can be changed, I believe
		}
		if (slowTimer > 0) {
			slowTimer-=Time.deltaTime;
		} else if (isSlowed && slowTimer<=0){
			speedx+=0.8f;
			isSlowed=false;
		}

		autoAttack ();
	}

	public void MagicDamage(int d)
	{
	
		health -= d;
	}

	public void PhysicalDamage(float d)
	{

		health -= d;
	}

	public void PureDamage(int d)
	{
		health -= d;
	}

	public void dRage(float r){
		rage += r;
	}
	void Attack(Status target)
	{
		target.PhysicalDamage((strength +attackDamage)* damagex);
	}
	void Rage(Status target){
		target.dRage (strength * damagex * 100f / maxHealth);
	}
	//void OnCollisionStay2D (Collision2D collider){
	void autoAttack(){//not necessarily melee range (uses range1), but this uses strength
		//closest = FindClosestEnemy();
		if (attackTimer<=0){
			//closest = FindClosestEnemyWalls(range1);
			closest = FindClosestEnemyWalls(range1);//
			if (closest != null){
				//try
				if (gameObject.tag == "Player" && getDistance(closest) < range1 
				    //&&//need if FindClosestEnemy w/o the Walls
				    ) 
				{
					Debug.Log ("AutoAttack1");
					Attack (closest.gameObject.GetComponent<Status> ());


					if (!closest.gameObject.GetComponent<Status> ().isRaged) {
						Rage(closest.gameObject.GetComponent<Status> ());
					}
					attackTimer = 1 / attackSpeed;
					GameObject swing = Instantiate(sword, transform.position, Quaternion.identity) as GameObject;
					swing.transform.parent = gameObject.transform;
				}
			}
			if (gameObject.tag=="Enemy" && getDistance(player) < range1 
			    //&& //
			    )
			{
				var heading = player.transform.position - gameObject.transform.position;
				var distance2 = heading.magnitude;
				var direction = heading/distance2;
				RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, range1, playerWalls);
				see = false;
				if (hit != null && hit.collider.tag == "Player"){
					see = true;
					//Debug.Log ("AutoAttack2");
					Attack (player.gameObject.GetComponent<Status> ());
				
					if (!player.gameObject.GetComponent<Status> ().isRaged){
						Rage(player.gameObject.GetComponent<Status> ());
					}
					player.gameObject.GetComponent<Player>().blood.Play();
					attackTimer = 1/attackSpeed;
				}
			}
		}
	}

	public GameObject FindClosestEnemy() {
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
	public GameObject FindClosestEnemyWalls(float range) {
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
					var heading = go.transform.position - transform.position;
					var distance2 = heading.magnitude;
					var direction = heading/distance2;
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, enemiesWalls);
					//Debug.Log (hit.collider.tag);
					if (hit){//implicit unity operator
						if(hit.collider.tag == "Enemy"){
						//Debug.Log (hit.collider.tag);
						closest = go;
						distance = curDistance;
						}
					}
				}
			}
		}
		if (closest != null) {
			return closest;
		} else {
			return null;
		}
	}
	//public GameObject FindPlayerWalls(float range) {}
	public float getDistance(GameObject go){
		return (go.transform.position - gameObject.transform.position).sqrMagnitude;
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

	public int getExperienceLeft(){
		return 100+10*level - (int)exp1;
	}
	public float getPercentExp(){
		return 100f * exp1 / (100f + 10f * (float) level);
	}

	public float getEffectiveSpeed(){
		return speedx * speed;
	}

    public float getSpeedx() {
        return speedx;
    }

    public void setSpeedx(float p) {
        speedx = p;
    }

    public int getPercentMana(){
        return (int) ((mana/maxMana + 0.0001) * 100);
    }

    public int getPercentHealth() {
        return (int) ((health/maxHealth + 0.0001) * 100);
    }
}
