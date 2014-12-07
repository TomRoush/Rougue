using UnityEngine;
using System.Collections;

public class Necklace : Equipment {
	public equipmentStats equipStats;
	public GameObject necklacePlayerReference;
	public Equipment adorner;//necklace adorns his lovely wife
	public bool canBePickedUp;
	void Start () {
		
		if (necklacePlayerReference == null) 
		{
			necklacePlayerReference = GameObject.FindGameObjectWithTag("Player");
			adorner = necklacePlayerReference.GetComponent<Equipment>();
		}

		
	}
	public Necklace (int strength, int magic, int speed)
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
				adorner.addNecklace(this);
				Destroy(this.gameObject);
				
			}
		}
		
		
		
	}
}
