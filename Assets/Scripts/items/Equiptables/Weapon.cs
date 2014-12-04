using UnityEngine;
using System.Collections;

public class Weapon : Equipment {

	public int strMod;
	public int mgkMod;
	public int spdMod;
	public string element;
	public GameObject weaponz;
	public Equipment wielder;
	public bool canBePickedUp;
	void Start () {

		if (weaponz == null) 
		{
			weaponz = GameObject.FindGameObjectWithTag("Player");
			wielder = weaponz.GetComponent<Equipment>();
		}


	}
	public Weapon (Weapon weapon)
	{
		this.strMod = weapon.strMod;
		this.mgkMod = weapon.mgkMod;
		this.spdMod = weapon.spdMod;
		this.element = weapon.element;
		canBePickedUp = false;
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
			if (Input.GetKeyDown (KeyCode.Space))
			{
				//wielder.addWeapon(this);
				//Destroy(this.gameObject);
			}
		}



	}
}
