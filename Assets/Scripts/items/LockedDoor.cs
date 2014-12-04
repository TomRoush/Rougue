using UnityEngine;
using System.Collections;

public class LockedDoor : items {

	public GameObject doorz;
	public items holder;
	public bool canBeInteractedWith = false;
	public int floorNum;
	public string type;


	void Start () {
	
		if (doorz == null) 
		{
			doorz = GameObject.FindGameObjectWithTag("Player");
			holder = doorz.GetComponent<items>();
		}
	}
	
	public LockedDoor()
	{
		floorNum = 1;

	}


	void OnTriggerEnter2D(Collider2D person)
	{
		if (person.gameObject.tag == "Player")
			
		{
			canBeInteractedWith = true;
		}
		
		
		
	}
	
	void OnTriggerExit2D(Collider2D person)
	{
		canBeInteractedWith = false;
		
	}




	void Update () 
	{

			if (canBeInteractedWith) 
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(holder.hasKey(this))
				{
					holder.takeKey(this);
					Destroy (this.gameObject);

				}

			}
			
		}
	}
}
