﻿using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

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

		speed = 13f;
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
		if (gameObject.tag == "Player") {
		//	damage1 = 25f; 
        	speed = 18f;
		}
		defense = 0.5f;//type 1 only defends against type1
		intelligence = 50f+level;
		resistence = 0.5f;
		
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
	}
	
	void OnCollisionStay2D (Collision2D collider){
		if (gameObject.tag=="Player" && collider.gameObject.tag == "Enemy" && attackTimer <= 0) {
			collider.gameObject.GetComponent<Status> ().health -= strength * damagex
				* collider.gameObject.GetComponent<Status> ().defense;

			if (!collider.gameObject.GetComponent<Status> ().isRaged){
				collider.gameObject.GetComponent<Status> ().rage += strength * damagex
					* collider.gameObject.GetComponent<Status> ().defense * 2;//gain twice rage as loss in hp
			}
			//money1+=0.1f;
			attackTimer = 1/attackSpeed;
		}
		if (gameObject.tag=="Enemy" && collider.gameObject.tag == "Player" && attackTimer <= 0) {
			collider.gameObject.GetComponent<Status> ().health -= strength * damagex
				* collider.gameObject.GetComponent<Status> ().defense;

			if (!collider.gameObject.GetComponent<Status> ().isRaged){
				collider.gameObject.GetComponent<Status> ().rage += strength * damagex
					* collider.gameObject.GetComponent<Status> ().defense * 2;//gain twice rage as loss in hp
			}
			collider.gameObject.GetComponent<Player>().blood.Play();
			attackTimer = 1/attackSpeed;
		}
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