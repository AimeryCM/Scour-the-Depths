using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ProjectUtil
{
	public static bool ItemsEqual(Item first, Item second, ItemDatabase database)
	{
		return database.GetID(first) == database.GetID(second);
	}

	public static void SwapBetweenInventories(IInventoryManager firstManager, int firstIndex, IInventoryManager secondManager, int secondIndex)
	{
		Inventory.InventoryInfo tempFrom1 = firstManager.Remove(firstIndex);
		Inventory.InventoryInfo tempFrom2 = secondManager.Replace(secondIndex, tempFrom1);
		firstManager.Replace(firstIndex, tempFrom2);
	}

	public static bool Sell(IInventoryManager seller, int sellIndex, IInventoryManager purchaser, int purIndex)
	{
		Debug.Log("Cost " + seller.GetCost(sellIndex));
		if(purchaser.GetCoins() >= seller.GetCost(sellIndex))
		{
			purchaser.ManageCoins(-1 * seller.GetCost(sellIndex));
			seller.ManageCoins(seller.GetCost(sellIndex));
			SwapBetweenInventories(seller, sellIndex, purchaser, purIndex);
			return true;
		}
		return false;
	}

	public static bool IsEquipmentSlot(int slot)
	{
		if(slot >= GlobalVariables.playerInventorySlots + GlobalVariables.hotbarSlots && slot < GlobalVariables.totalPlayerInventorySlots)
			return true;
		return false;
	}

	public static bool IsWeaponSlot(int slot)
	{
		if(slot >= GlobalVariables.playerInventorySlots + GlobalVariables.hotbarSlots && slot < GlobalVariables.playerInventorySlots + GlobalVariables.hotbarSlots + GlobalVariables.weaponSlots)
			return true;
		return false;
	}

	public static bool IsTrinketSlot(int slot)
	{
		if(slot >= GlobalVariables.playerInventorySlots + GlobalVariables.hotbarSlots + GlobalVariables.weaponSlots && slot < GlobalVariables.totalPlayerInventorySlots)
			return true;
		return false;
	}

	public static string ArrayToString<T>(T[] array)
	{
		StringBuilder builder = new StringBuilder();
		builder.Append("[");
		for(int x = 0; x < array.Length; x++)
		{
			if(x != 0)
				builder.Append(", ");
			builder.Append(array[x].ToString());
		}
		builder.Append("]");
		return builder.ToString();
	}
}
