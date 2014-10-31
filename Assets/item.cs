
using UnityEngine;
using System.Collections;
[System.Serializable]
public class item{
	public string itemName;
	public int itemID;
	public string itemDescription;
	public Texture2D itemIcon;
	public int itemPowerModifier;
	public int itemSpeedModifier;
	public int itemMagicModifier;
	public ItemType itemType;


	public enum ItemType
	{
		Weapon, Consumable, Armor, Misc


	}

	public item(string name, int ID, string desc, int power, int speed, int magic, ItemType type)
	{
		itemName = name;
		itemID = ID;
		itemDescription = desc;
		itemIcon = (Texture2D)Resources.Load<Texture2D>(name);

		itemPowerModifier = power;
		itemSpeedModifier = speed;
		itemMagicModifier = magic;
		itemType = type;
		
	}

	public item()
	{


	}
	//to do by wednesday, pick up a potion increment number use a potion decrement number
	//have a bag that lets you pick stuff up and put it in
	//shop
}
