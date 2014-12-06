using UnityEngine;
using System.Collections;

public class Helmet : Equipment {
	public equipmentStats equipStats;
	public GameObject helmetPlayerReference;
	public Equipment wearer;
	public bool canBePickedUp;
	void Start () {
		
		if (helmetPlayerReference == null) 
		{
			helmetPlayerReference = GameObject.FindGameObjectWithTag("Player");
			wearer = helmetPlayerReference.GetComponent<Equipment>();
		}
		
		
	}
	public Helmet (int strength, int magic, int speed)
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
				wearer.addHelmet(this);
				Destroy(this.gameObject);
				
			}
		}
		
		
		
	}
}
