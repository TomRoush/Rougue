using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class items : MonoBehaviour {
	public GameObject itemzYo;
	public items owner;
	public int bronzeKeys;
	public int silverKeys;
	public int goldKeys;
	public int numHealthPotions = 0;



	void Start ()
	{
		if (itemzYo == null) 
		{
			itemzYo = GameObject.FindGameObjectWithTag("Player");
			owner = itemzYo.GetComponent<items>();
		}

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	public void AddKey(Keys keyToAdd)
	{
		if (keyToAdd.type == "bronze")
		{
			owner.bronzeKeys++;
		}

		if (keyToAdd.type == "silver")
		{
			owner.silverKeys++;
		}

		if (keyToAdd.type == "gold")
		{
			owner.goldKeys++;
		}

	}
	public bool hasKey(LockedDoor doorToCheck)
	{
		if(doorToCheck.type == "bronze" && bronzeKeys > 0)
		{
			return true;
		}

		if(doorToCheck.type == "silver" && silverKeys > 0)
		{
			return true;
		}

		if(doorToCheck.type == "gold" && goldKeys > 0)
		{
			return true;
		}
		return false;
	}

	public void takeKey(LockedDoor doorToDelete)
	{
		if(doorToDelete.type == "bronze")
		{
			bronzeKeys--;
		}

		if(doorToDelete.type == "silver")
		{
			silverKeys--;
		}

		if(doorToDelete.type == "gold")
		{
			goldKeys--;
		}
	}

}

