using UnityEngine;
using System.Collections;

public class Weapon : Equipment {
	public int strength;
	public equipmentStats equipStats;
	public string element;
	public GameObject weaponPlayerReference;
	public Equipment wielder;
	public bool canBePickedUp;
	void Start () {

		if (weaponPlayerReference == null) 
		{
			weaponPlayerReference = GameObject.FindGameObjectWithTag("Player");
			wielder = weaponPlayerReference.GetComponent<Equipment>();
		}


	}
	public Weapon (int strength, int magic, int speed)
	{
		//equipmentStats.strMod = weapon.strMod;
		equipStats.str = strength;
		equipStats.intelligence = magic;
		equipStats.agility = speed;
		canBePickedUp = false;
	}

	public void setStats(int strength, int magic, int speed)
	{
		equipStats = new equipmentStats(strength, magic, speed);
	}

	void OnTriggerEnter2D(Collider2D person)
	{
		if (person.gameObject.tag == "Player")
			
		{
			canBePickedUp = true;
		}
		
		
		
	}
	
	void OnTriggerExit2D(Collider2D person)
	{
		canBePickedUp = false;
		
	}
	

	void Update ()
	{
		if (canBePickedUp) 
		{
			if (Input.GetButtonDown("Action"))
			{
				wielder.addWeapon(this);
				Destroy(this.gameObject);

			}
		}



	}
}
