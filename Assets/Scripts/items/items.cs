using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class items : MonoBehaviour {
	public List<int> numKeysByFloor = new List<int>();
	public int numHealthPotions = 0;



	void Start ()
	{
		for(int i = 0; i < 10; i++)
		{
			numKeysByFloor.Add (0);
		}

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	public void AddKey(Keys keyToAdd)
	{

		numKeysByFloor[keyToAdd.floorNumber] = numKeysByFloor[keyToAdd.floorNumber]+1;

	}
	public bool hasKey(LockedDoor doorToCheck)
	{
		if (numKeysByFloor.ToArray()[doorToCheck.floorNum] > 0)
		{
			return true;
		} 

		else
		{
			return false;
		}
	}

	public void takeKey(LockedDoor doorToDelete)
	{
		numKeysByFloor[doorToDelete.floorNum] = numKeysByFloor[doorToDelete.floorNum]-1;
	}

}

