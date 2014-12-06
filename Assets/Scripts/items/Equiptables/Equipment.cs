using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {
	
	public GameObject equipmentPlayerRefrence;
	public Player equipper;
	public equipmentStats weaponStats;
	public int str;
	public int agility;
	public int intelligence;
	void Start () 
	{
		if (equipmentPlayerRefrence == null) 
		{
			equipmentPlayerRefrence = GameObject.FindGameObjectWithTag("Player");
			equipper = equipmentPlayerRefrence.GetComponent<Player>();
		}

		weaponStats = new equipmentStats (str,agility,intelligence);
	}


	void Update () {
	
	}

	public void addWeapon(Weapon weapon)
	{
		equipper.equippedSword  = weaponStats;
		print (equipper.equippedSword);

	}
}
