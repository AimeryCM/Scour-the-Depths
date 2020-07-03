using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : Inventory
{
	private Weapon[] weapons = null;
	private Trinket[] trinkets = null;

	public override void Setup()
	{
		base.Setup();
		weapons = new Weapon[GlobalVariables.weaponSlots];
		trinkets = new Trinket[GlobalVariables.trinketSlots];
	}

	public override InventoryInfo GetItem(int pos)
	{
		if(pos < base.size)
			return base.GetItem(pos);
		if(pos < base.size + weapons.Length)
			return new InventoryInfo(weapons[pos - base.size], 1);
		if(pos < base.size + weapons.Length + trinkets.Length)
			return new InventoryInfo(trinkets[pos - base.size - weapons.Length], 1);
		return new InventoryInfo(null, 0);
	}

	public override InventoryInfo Remove(int slot)
	{
		if(slot < size)
			return base.Remove(slot);
		if(slot < base.size + weapons.Length)
		{
			Item temp = weapons[slot - size];
			weapons[slot - size] = null;
			return new InventoryInfo(temp, 1);
		}
		if(slot < base.size + weapons.Length + trinkets.Length)
		{
			Item temp = trinkets[slot - size - weapons.Length];
			trinkets[slot - size - weapons.Length] = null;
			return new InventoryInfo(temp, 1);
		}
		return new InventoryInfo(null, 0);
	}

	public override InventoryInfo Replace(int slot, InventoryInfo otherInfo)
	{
		if(slot < size)
			return base.Replace(slot, otherInfo);
		if(slot < base.size + weapons.Length && (!otherInfo.occupied || otherInfo.item.itemType == ItemType.Weapon))
		{
			Item temp = weapons[slot - size];
			weapons[slot - size] = otherInfo.occupied ? (Weapon)otherInfo.item : null;
			return new InventoryInfo(temp, 1);
		}
		if(slot < base.size + weapons.Length + trinkets.Length && otherInfo.item.itemType == ItemType.Trinket)
		{
			Item temp = trinkets[slot - size - weapons.Length];
			trinkets[slot - size - weapons.Length] = otherInfo.occupied ? (Trinket)otherInfo.item : null;
			return new InventoryInfo(temp, 1);
		}
		return new InventoryInfo(null, 0);
	}

	public bool AddWeapon(Weapon weap)
	{
		for(int x = 0; x < weapons.Length; x++)
		{
			if(weapons[x] == null)
			{
				weapons[x] = weap;
				return true;
			}
		}
		return false;
	}

	public bool AddWeapon(Weapon weap, int index)
	{
		if(index < weapons.Length && weapons[index] == null)
		{
			weapons[index] = weap;
			return true;
		}
		return false;
	}

	public Weapon GetWeapon(int index)
	{
		if(index >= 0 && index < weapons.Length)
			return weapons[index];
		return null;
	}

	public Weapon RemoveWeapon(int index)
	{
		if(index < weapons.Length && index <= 0)
		{
			Weapon result = weapons[index];
			weapons[index] = null;
			return result;
		}
		return null;
	}

	public Weapon ReplaceWeapon(int index, Weapon weap)
	{
		Weapon result = RemoveWeapon(index);
		if(result != null)
		{
			AddWeapon(weap, index);
		}
		return result;
	}

	public bool AddTrinket(Trinket trin)
	{
		for(int x = 0; x < trinkets.Length; x++)
		{
			if(trinkets[x] == null)
			{
				trinkets[x] = trin;
				return true;
			}
		}
		return false;
	}

	public bool AddTrinket(Trinket trin, int index)
	{
		if(index < trinkets.Length && trinkets[index] == null)
		{
			trinkets[index] = trin;
			return true;
		}
		return false;
	}

	public Trinket GetTrinket(int index)
	{
		if(index >= 0 && index < trinkets.Length)
			return trinkets[index];
		return null;
	}

	public Trinket RemoveTrinket(int index)
	{
		if(index < trinkets.Length && index <= 0)
		{
			Trinket result = trinkets[index];
			trinkets[index] = null;
			return result;
		}
		return null;
	}

	public Trinket ReplaceTrinket(int index, Trinket trinket)
	{
		Trinket result = RemoveTrinket(index);
		if(result != null)
		{
			AddTrinket(trinket, index);
		}
		return result;
	}

	public override bool Swap(int pos1, int pos2)
	{
		if(pos1 < base.size && pos2 < base.size)
			return base.Swap(pos1, pos2);
		if(pos1 >= base.size && pos2 >= base.size)
			return false;
		if(pos1 >= base.size && pos1 < base.size + weapons.Length)
		{
			Debug.Log("Here1");
			if(!itemList[pos2].occupied || itemList[pos2].item.itemType == ItemType.Weapon)
			{
				Item temp = weapons[pos1 - base.size];
				weapons[pos1 - base.size] = itemList[pos2].occupied ? (Weapon)itemList[pos2].item : null;
				itemList[pos2] = temp == null ? new InventoryInfo(null, 0) : new InventoryInfo(temp, 1);
				return true;
			}
			return false;
		}
		if(pos1 >= base.size + weapons.Length && pos1 < base.size + weapons.Length + trinkets.Length)
		{
			Debug.Log("Here2");
			if(!itemList[pos2].occupied || itemList[pos2].item.itemType == ItemType.Trinket)
			{
				Item temp = trinkets[pos1 - base.size - weapons.Length];
				trinkets[pos1 - base.size - weapons.Length] = itemList[pos2].occupied ? (Trinket)itemList[pos2].item : null;
				itemList[pos2] = temp == null ? new InventoryInfo(null, 0) : new InventoryInfo(temp, 1);
				return true;
			}
			return false;
		}
		if(pos2 >= base.size && pos2 < base.size + weapons.Length)
		{
			Debug.Log("Here3");
			if(!itemList[pos1].occupied || itemList[pos1].item.itemType == ItemType.Weapon)
			{
				Item temp = weapons[pos2 - base.size];
				weapons[pos2 - base.size] = itemList[pos1].occupied ? (Weapon)itemList[pos1].item : null;
				itemList[pos1] = temp == null ? new InventoryInfo(null, 0) : new InventoryInfo(temp, 1);
				return true;
			}
			return false;
		}
		if(pos2 >= base.size + weapons.Length && pos2 < base.size + weapons.Length + trinkets.Length)
		{
			Debug.Log("Here4");
			if(!itemList[pos1].occupied || itemList[pos1].item.itemType == ItemType.Trinket)
			{
				Item temp = trinkets[pos2 - base.size - weapons.Length];
				trinkets[pos2 - base.size - weapons.Length] = itemList[pos1].occupied ? (Trinket)itemList[pos1].item : null;
				itemList[pos1] = temp == null ? new InventoryInfo(null, 0) : new InventoryInfo(temp, 1);
				return true;
			}
			return false;
		}
		return false;
	}
}
