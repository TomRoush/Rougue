using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class Player : Entities {

	public bool paused;
	private Texture2D scroll;

	private int previousDirection;
	private float velocity;
	public bool alive = true;
	public float curHealth;
	private MakeMap Dungeon;
	public equipmentStats equippedSword;
	public equipmentStats equippedArmor;
	public equipmentStats equippedHelmet;
	public equipmentStats equippedNecklace;

	float floorChangeTime = 0.0f;
	
	Animator anim;

	Transform weapon;

	public ParticleSystem blood;//turned public
	public GameObject bloodSpatter;

	// GUI Object
	private PlayerGUI playerGUI;

	private Image actionButton;
	private Text actionText;

	private void Start() {
        
		InitializeEntity();
		SelfCast = new Heal(gameObject);
		SelfCast2 = new PoisonCloud(gameObject);
		AutoTarget = new Fireball(gameObject);
		AutoTarget2 = new Mudball(gameObject);
		PosTarget = new MagicMissle(gameObject);

		anim = GetComponent<Animator>();
		curHealth = gameObject.GetComponent<Status>().getHealth();
		blood = transform.Find("Blood").GetComponent<ParticleSystem>();

		Dungeon = GameObject.Find("MapGenerator").GetComponent<MakeMap>();

		actionButton = GameObject.Find("ActionButton").GetComponent<Image>();
		actionText = GameObject.Find("ActionText").GetComponent<Text>();

		weapon = transform.Find("Weapon");

		playerGUI = new PlayerGUI(this);


		paused = false;
		Time.timeScale = 1;

	}

	private void FixedUpdate() { 
//		actionText.text = "Use";

		//walkDirection: 1 = left, 2 = up, 3 = right, 4 = down;
		//idleDirection: saves previous walkDirection to animate idle
		float dx = 0;
		float dy = 0;
		if(!gameObject.GetComponent<Status>().isStunned) {
			if(CrossPlatformInputManager.GetAxisRaw("Vertical") > 0.75) {
				anim.SetInteger("direction", 2);
				anim.SetFloat("velocity", 1.0f);
				previousDirection = 2;
				dy = 1;
			}
			if(CrossPlatformInputManager.GetAxisRaw("Horizontal") < -0.75) {
				//anim.SetBool("a", true);
				//anim.SetBool("d", false);

				anim.SetInteger("direction", 1);
				anim.SetFloat("velocity", 1.0f);
				previousDirection = 3;
				dx = -1;
			}
			if(CrossPlatformInputManager.GetAxisRaw("Vertical") < -0.75) {
//				if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow)) 
				anim.SetInteger("direction", 4);
				anim.SetFloat("velocity", 1.0f);
				previousDirection = 4;
				dy = -1;
			}
			if(CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0.75) {			
//				Vector3 theScale = transform.localScale;
//				theScale.x = 1;
//				transform.localScale = theScale;

				anim.SetInteger("direction", 1);
				anim.SetFloat("velocity", 1.0f);
				previousDirection = 1;
				dx = 1;
			}

			if(CrossPlatformInputManager.GetButton("Jump")) {
				Debug.Log("Jump");
				SelfCast.cast(gameObject);
			}

			if(CrossPlatformInputManager.GetButton("Heal")) {
				Debug.Log("Heal");
				SelfCast.cast(gameObject);
			}

			if(CrossPlatformInputManager.GetButton("PoisonCloud")) {
				Debug.Log("Cloud");
				SelfCast2.cast(gameObject);
			}

			if(CrossPlatformInputManager.GetButton("Fireball")) {
				Debug.Log("Fire");
				AutoTarget.cast(cStat.FindClosestEnemy());
			}

			if(CrossPlatformInputManager.GetButton("Mudball")) {
				Debug.Log("Mud");
				AutoTarget2.cast(cStat.FindClosestEnemy());
			}
//			if(Input.GetMouseButtonDown(0)) {
//				PosTarget.cast(Input.mousePosition);
//
//			}
            
		}
		//if not moving
		if(!PlayerInput.isMoving()) {
			anim.SetInteger("direction", previousDirection);
			anim.SetFloat("velocity", 0.0f);
		}

		this.setDirection(new Vector3(dx, dy, 0));
		Move();

		if(CrossPlatformInputManager.GetButton("Cancel")) {
			if(paused) {
				paused = false;
				playerGUI.paused = false;
			} else {
				paused = true;
				playerGUI.paused = true;
			}
		}
		
		if(gameObject.GetComponent<Status>() != null && gameObject.GetComponent<Status>().getHealth() < 0) {
			Die();		
		}

		UpdateGameState();
	}

	//NEEDS TO BE CALLED
	
	public void UpdateGameState() {
		Time.timeScale = paused ? 0 : 1;

	}
	
	public override void takeHealth(int amount) {
		health = health - amount;
		Debug.Log("health left" + health);
		blood.Play();
		if(Random.value < 0.25f) {
			Instantiate(bloodSpatter, transform.position, Quaternion.identity);
		}
		
		if(health <= 0) {
			Die();
		}
	}

	private void OnGUI() {
		playerGUI.onGUI();
//		actionText.text = "Use";
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("goal")) {
			actionButton.color = new Color(actionButton.color.r, actionButton.color.g, actionButton.color.b, 1.0f);
			actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 1.0f);
			actionText.text = "Down";
			if(CrossPlatformInputManager.GetButton("Item") && Time.time - floorChangeTime > 0.5f) {
				floorChangeTime = Time.time;
				Dungeon.NextFloor();
			}
		} else if(other.CompareTag("UpStairs")) {
			actionButton.color = new Color(actionButton.color.r, actionButton.color.g, actionButton.color.b, 1.0f);
			actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 1.0f);
			actionText.text = "Up";
			if(CrossPlatformInputManager.GetButton("Item") && Time.time - floorChangeTime > 0.5f) {
				floorChangeTime = Time.time;
				Dungeon.PreviousFloor();
			}
		} else if(other.CompareTag("Equip") || other.CompareTag("Item")) {
			actionButton.color = new Color(actionButton.color.r, actionButton.color.g, actionButton.color.b, 1.0f);
			actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 1.0f);
			actionText.text = "Use";
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		actionText.text = "Use";
		if(other.CompareTag("goal") || other.CompareTag("UpStairs") || other.CompareTag("Equip") || other.CompareTag("Item")) {
			actionButton.color = new Color(actionButton.color.r, actionButton.color.g, actionButton.color.b, 0.5f);
			actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 0.5f);
		}
	}

	public void Respawn(Vector3 spawnPt) {
		transform.position = spawnPt;
	}

	public override void Die() {
		alive = false;
		paused = true;
		playerGUI.alive = false;
		UpdateGameState();
	}

	public void refreshEquipStats() {
		cStat.clearEquip();
		addStatsFromItem(equippedSword);
		addStatsFromItem(equippedArmor);
		addStatsFromItem(equippedHelmet);
		addStatsFromItem(equippedNecklace);
		cStat.refreshStats();
        
	}

	public void addStatsFromItem(equipmentStats equip) {
		if(equip != null) {
			cStat.equipStrength += equip.strength;
			cStat.equipAgility += equip.agility;
			cStat.equipIntelligence += equip.intelligence;
		}
	}
}
