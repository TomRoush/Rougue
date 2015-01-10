using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;

public class Potions : MonoBehaviour {

	public string type;
	public GameObject potionPlayerReference;
	public bool puedoBePickedUp = false;
	public Status tStat;

	void Start() {
		if(potionPlayerReference == null) {
			potionPlayerReference = GameObject.FindGameObjectWithTag("Player");
			tStat = potionPlayerReference.GetComponent<Status>();
		}
	}
	

	void OnTriggerEnter2D(Collider2D person) {
		if(person.gameObject.tag == "Player") {
			puedoBePickedUp = true;
		}
		
		
		
	}

	void OnTriggerExit2D(Collider2D person) {
		puedoBePickedUp = false;
		
	}

	void Update() {

		if(puedoBePickedUp) {
			if(CrossPlatformInputManager.GetButton("Item")) {
				GameObject hot = Resources.Load("BuffRegen") as GameObject;
				hot.GetComponent<TimedEffect>().target = potionPlayerReference;
				hot.GetComponent<BuffRegen>().healthBuff = tStat.getMaxHealth() / 2;
				hot.GetComponent<BuffRegen>().manaBuff = tStat.getMaxMana() / 2;
				GameObject.Instantiate(hot, potionPlayerReference.transform.position, Quaternion.identity);	
				Destroy(this.gameObject);
			}
			
		}


	}

}
