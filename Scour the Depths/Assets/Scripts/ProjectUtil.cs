using System.Collections;
using System.Collections.Generic;
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
}
