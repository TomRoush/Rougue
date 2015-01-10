using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;

public abstract class Equipment : MonoBehaviour {

	public Player PlayerReference;
	public equipmentStats eqStats;
	public int strength;
	public int agility;
	public int intelligence;
	public bool canBePickedUp;

	void Start() {
		PlayerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		;
		canBePickedUp = false;
		if(eqStats == null) {
			eqStats = new equipmentStats(strength, agility, intelligence);
		}
	}

	public void setStats(int str, int agi, int intl) {
		Debug.Log(str);
		strength = str;
		agility = agi;
		intelligence = intl;
		if(eqStats == null) {
			eqStats = new equipmentStats(str, agi, intl);
		} else {
			eqStats.strength = str;
			eqStats.agility = agi;
			eqStats.intelligence = intl;

		}
	}

	void OnTriggerExit2D(Collider2D person) {
		if(person.gameObject.tag == "Player") {
			canBePickedUp = false;
		}
	}

	void OnTriggerEnter2D(Collider2D person) {
		if(person.gameObject.tag == "Player") {
			canBePickedUp = true;
		}
	}

	public void Update() {
		if(canBePickedUp && CrossPlatformInputManager.GetButtonDown("Item")) {
			addEquipment();
			Destroy(gameObject);
		}
	}

	public abstract void addEquipment();

}
