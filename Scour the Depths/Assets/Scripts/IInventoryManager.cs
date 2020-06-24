using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryManager
{
	void ManageCoins(int change);

	int GetCoins();

	bool AddToInventory(Item item);

	Inventory.InventoryInfo Remove(int slot);

	Inventory.InventoryInfo Replace(int slot, Inventory.InventoryInfo info);

	bool Swap(int slot1, int slot2);

	void ToggleInventory();

	void Setup(List<Inventory.InventoryInfo> items);

	int GetCost(int index);
}