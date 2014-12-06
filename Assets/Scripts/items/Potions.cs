using UnityEngine;
using System.Collections;

public class Potions : items {

	public string type;
	public GameObject potionPlayerReference;
	public items holder;
	public bool puedoBePickedUp = false;

	void Start () {
		if (potionPlayerReference == null) 
		{
			potionPlayerReference = GameObject.FindGameObjectWithTag("Player");
			holder = potionPlayerReference.GetComponent<items>();
		}
	}
	

	void OnTriggerEnter2D(Collider2D person)
	{
		if (person.gameObject.tag == "Player")
			
		{
			puedoBePickedUp = true;
		}
		
		
		
	}

	void OnTriggerExit2D(Collider2D person)
	{
		puedoBePickedUp = false;
		
	}

	void Update ()
	{

		if (puedoBePickedUp) 
			
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				holder.numHealthPotions++;
				Destroy(this.gameObject);
			}
			
		}


	}

}
