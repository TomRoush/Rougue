using UnityEngine;
using System.Collections;

public class Armor : Equipment {
	public equipmentStats equipStats;
	public GameObject armorPlayerReference;
	public Equipment donner; // donned his armor
	public bool canBePickedUp;
	void Start () {
		
		if (armorPlayerReference == null) 
		{
			armorPlayerReference = GameObject.FindGameObjectWithTag("Player");
			donner = armorPlayerReference.GetComponent<Equipment>();
		}
		
		
	}
	public Armor (int strength, int magic, int speed)
	{
		equipStats.str = strength;
		equipStats.intelligence = magic;
		equipStats.agility = speed;
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
				donner.addArmor(this);
				Destroy(this.gameObject);
				
			}
		}
		
		
		
	}
}
