using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
	[System.Serializable]
	public struct InventoryInfo
	{
		public Item item;
		public int quantity;
		public bool occupied;
		public InventoryInfo(Item i, int quant)
		{
			item = i;
			quantity = quant;
			if(quant > 0)
				occupied = true;
			else
				occupied = false;
		}

		public override string ToString()
		{
			if(!occupied)
				return "Unoccupied";
			return quantity + " copies of " + item.itemName;
		}
	}

	public int size = 16;
	public int startingCoins = 0;
	private int coins = 0;
	public ItemDatabase database = null;
	private InventoryInfo[] itemList = null;

	public void Setup()
	{
		itemList = new InventoryInfo[size];
	}

	public void Setup(List<InventoryInfo> input)
	{
		Setup();
		for(int x = 0; x < input.Count && x < size; x++)
		{
			itemList[x] = input[x];
		}
	}

	public InventoryInfo GetItem(int pos)
	{
		if(pos >= 0 && pos < size)
			return itemList[pos];
		return new InventoryInfo(null, 0);
	}

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
		coins = startingCoins;
	}

	public InventoryInfo Replace(int index, InventoryInfo otherInfo)
	{
		InventoryInfo result = itemList[index];
		itemList[index] = otherInfo;
		return result;
	}

	//adds 1 of the item to the inventory
	public int Add(Item item)
	{
		return Add(item, 1);
	}

	public int Add(Item item, int amount)
	{
		if(itemList == null || itemList.Length != size)
			Setup();
		int openSpot = -1;
		Debug.Log("itemList[].Length: " + itemList.Length + " size: " + size);
		for(int x = 0; x < size; x++)
		{
			if(itemList[x].occupied)
			{
				if(ProjectUtil.ItemsEqual(item, itemList[x].item, database))
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
			itemList[openSpot].occupied = true;
			return openSpot;
		}
		return -1;
	}

	//removes all of the item
	public bool Remove(Item item)
	{
		return Remove(item, int.MaxValue);
	}

	public bool Remove(Item item, int amount)
	{
		for(int x = 0; x < size; x++)
		{
			if(itemList[x].occupied){
				if(ProjectUtil.ItemsEqual(item, itemList[x].item, database))
				{
					itemList[x].quantity -= amount;
					if(itemList[x].quantity <= 0)
					{
						itemList[x] = new InventoryInfo(null, 0);
					}
					return true;
				}
			}
		}
		return false;
	}

	public InventoryInfo Remove(int slot)
	{
		InventoryInfo temp = itemList[slot];
		itemList[slot] = new InventoryInfo(null, 0);
		return temp;
	}

	public bool Swap(int pos1, int pos2)
	{
		if(itemList == null)
			Setup();
		if(pos1 < size && pos2 < size)
		{
			InventoryInfo temp = itemList[pos1];
			itemList[pos1] = itemList[pos2];
			itemList[pos2] = temp;
			return true;
		}
		return false;
	}

	public override string ToString()
	{
		string output = "Size: " + size + "\n";
		for(int x = 0; x < size; x++)
		{
			output += x + ": " + itemList[x].ToString() + "\n";
		}
		return output;
	}
}
