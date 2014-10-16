using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {
	
	public float speed;
	public float speedx;//speed-buff or slow-debuff
	public float agility;//turn speed
	public float agilityx;
	
	public float health;
	public float healthRegen;
	
	public float rage;
	public float rageDecay;
	public float rageTimer;
	public bool isRaged;
	
	public float attackSpeed;
	public float attackTimer;

	public float damagex;
	public float damage1;//type 1
	public float defense1;//type 1
	public float damage2;
	public float defense2;
	
	public bool isStunned;
	
	// Use this for initialization
	void Start () {
		speed = 8f;
		speedx = 1f;//speed-buff or slow-debuff
		agility = 180f;
		agilityx = 1f;
		
		health = 100f;
		healthRegen = 1f;
		
		rage = 0f;
		rageDecay = 3;
		rageTimer = 0;
		isRaged = false;
		
		attackSpeed = 1f;
		attackTimer = 0;

		damagex = 1f;
		damage1 = 20f;//type 1
		defense1 = 0.5f;//type 1 only defends against type1
		damage2 = 50f;
		defense2 = 0.5f;
		
		isStunned = false;
		
		Debug.Log (speed);
		Debug.Log (damage1);
		//Debug.Log (collider.gameObject.GetComponent<Status>().damage1);
		Debug.Log (defense1);
		//Debug.Log (collider.gameObject.GetComponent<Status>().damage1*defense1/300);
		Debug.Log (damage2);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
		}
		
		if (health>100){
			health=100;
		}else if (health<100){
			health += Time.deltaTime * healthRegen;
		}
		
		if (rage < 0) {
			rage=0;
		}else if (rage>=100){//rage on
			speedx+=1f;//speed does not work
			damagex+=10f;//damage works though
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
			isRaged=false;
			rageTimer=0;
		}
		
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}
	}
	
	void OnCollisionStay2D (Collision2D collider){
		if (attackTimer <= 0) {
			if (gameObject.tag == "Player" && collider.gameObject.tag == "Enemy") {
				health -= collider.gameObject.GetComponent<Status> ().damage1 
					* collider.gameObject.GetComponent<Status> ().damagex
					* defense1;
				if (!isRaged){
					rage += collider.gameObject.GetComponent<Status> ().damage1 
						* collider.gameObject.GetComponent<Status> ().damagex
						* defense1 * 2;
				}
				attackTimer = 1/attackSpeed;
			}
			if (gameObject.tag == "Enemy" && collider.gameObject.tag == "Player") {
				health -= collider.gameObject.GetComponent<Status> ().damage1 
					* collider.gameObject.GetComponent<Status> ().damagex
					* defense1;
				if (!isRaged){
					rage += collider.gameObject.GetComponent<Status> ().damage1 
						* collider.gameObject.GetComponent<Status> ().damagex
						* defense1 * 2;
				}
				attackTimer = 1/attackSpeed;
			}
		}
	}

	public float getSpeedx(){
		return speedx;
	}
}
