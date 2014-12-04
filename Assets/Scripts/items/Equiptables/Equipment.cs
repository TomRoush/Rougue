using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public Weapon weapon;
	//public Armor armor;
	//public Necklace necklace;
	//public Helmet helmet;
	public GameObject stuffz;
	public Equipment equipper;
	void Start () 
	{
		if (stuffz == null) 
		{
			stuffz = GameObject.FindGameObjectWithTag("Player");
			equipper = stuffz.GetComponent<Equipment>();
		}


	}
	

	void Update () {
	
	}

	//public void addWeapon(Weapon weapon)
	//{
	//	this.weapon = new Weapon(weapon);

	//}
}
