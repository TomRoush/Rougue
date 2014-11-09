using UnityEngine;
using System.Collections;

public class Keys : items  {

	public int floorNumber;
	private bool canBePickedUp = false;
	public GameObject itemz;
	public items holder;

	public Keys(int num)
	{
		floorNumber = num;	
	}

	void Start()
	{
		if (itemz == null) 
		{
			itemz = GameObject.FindGameObjectWithTag("Player");
			holder = itemz.GetComponent<items>();
		}


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
				holder.AddKey(this);
				Destroy(this.gameObject);
			}
		}
	}



}
