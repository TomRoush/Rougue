using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int column, row;
	public List<item> inventory = new List<item>();
	public List<item> slots = new List<item>();
	public GUISkin inventorySlotsSkin;
	private itemDatabase database;
	private bool displayInventory = false;
	public Entities holding;




	void Start () {




		for (int i = 0; i < (column*row); i++)
		{
			slots.Add(new item());
			inventory.Add(new item());

		}

		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<itemDatabase>();
		AddItem (0);

	}
	
	void Update()
	{
		if(Input.GetKeyDown (KeyCode.I))
		{
			displayInventory = !displayInventory;

		}

		if (Input.GetKeyDown (KeyCode.P))
		{
			UsePotion();	
		}


	}

	void UsePotion()
	{
		if (InventoryContains (0))
		{
			holding.giveHealth(100);
			RemoveItem(0);

		}


	}
	
	void OnGUI ()
	{
		GUI.skin = inventorySlotsSkin;
		if(displayInventory)
		{
			DrawInventory();
		}


		
		for(int i = 0; i < inventory.Count; i++)
		{

			GUI.Label(new Rect (10,i * 20,200,50), inventory[i].itemName);

		}

	}

	void DrawInventory()
	{
		int i = 0;
		for(int y = 0; y < column; y++)
		{

			for(int x = 0; x < row; x++)
			{
				Rect slotLocation = new Rect(150 + x * 75,y * 75, 50, 50);
				GUI.Box(new Rect(150 + x * 75,y * 75, 50, 50),"",inventorySlotsSkin.GetStyle("Slot"));

				slots[i] = inventory[i];
				if(slots[i].itemID != null)
				{
					GUI.DrawTexture(slotLocation,slots[i].itemIcon);

				}

				i++;	
			}




		}


	}

	void AddItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++) 
		{
			if(inventory[i].itemName == null)
			{
				for(int j = 0; j < database.items.Count; j++)
				{
					if(database.items[j].itemID == id)
					{

						inventory[i] = database.items[j];
					}

				}
				break;
			}


		}

	}

	void RemoveItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++) 
		{
			if(inventory[i].itemID == id)
			{
				inventory[i] = new item();
				break;
			}
			

		}


	}

	bool InventoryContains(int id)
	{
		bool result = false;
		for(int i = 0; i < inventory.Count; i++)
		{
			if(inventory[i].itemID == id)
			{
				result = true;
			}


		}
		return result;

	}

}
