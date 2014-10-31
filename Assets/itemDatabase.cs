using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemDatabase : MonoBehaviour {
	public List<item> items = new List<item>();

	void Start()
	{
		items.Add (new item ("RedPotion", 0, "do not take if pregnant", 0,0,1,item.ItemType.Consumable));

		//pass in list of modifiers
	}
}
