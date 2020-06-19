using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
	public struct InventoryInfo
	{
		public Item item;
		public int quantity;
		public bool occupied;
		public InventoryInfo(Item i, int quant)
		{
			item = i;
			quantity = quant;
			occupied = false;
		}
	}

	public int size = 16;
	private int coins;
	private InventoryInfo[] itemList;

	public int ManageCoins(int amount)
	{
		coins = Mathf.Clamp(coins + amount, 0, int.MaxValue);
		return coins;
	}

	public int GetCoins()
	{
		return coins;
	}

	public void ResetCoins()
	{
		coins = 0;
	}

	//adds 1 of the item to the inventory
	public int Add(Item item)
	{
		return Add(item, 1);
	}

	public int Add(Item item, int amount)
	{
		if(itemList == null)
			itemList = new InventoryInfo[size];
		int openSpot = -1;
		for(int x = 0; x < size; x++)
		{
			if(itemList[x].occupied)
			{
				if(itemList[x].item.itemName.Equals(item.itemName))
				{
					itemList[x].quantity += amount;
					return x;
				}
			}
			if(openSpot == -1 && itemList[x].item == null)
			{
				openSpot = x;
			}
		}
		if(openSpot != -1)
		{
			itemList[openSpot] = new InventoryInfo(item, amount);
			return openSpot;
		}
		return -1;
	}

	public bool Remove(Item item)
	{
		for(int x = 0; x < size; x++)
		{
			if(itemList[x].item.itemName.Equals(item.itemName))
			{
				itemList[x] = new InventoryInfo(null, 0);
				return true;
			}
		}
		return false;
	}

	public bool Remove(Item item, int amount)
	{
		for(int x = 0; x < size; x++)
		{
			if(itemList[x].item.itemName.Equals(item.itemName))
			{
				itemList[x].quantity -= amount;
				if(itemList[x].quantity <= 0)
					itemList[x] = new InventoryInfo(null, 0);
				return true;
			}
		}
		return false;
	}
}
