using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventoryManager : MonoBehaviour, IInventoryManager
{
	public Inventory inventory = null;
	public InventoryOwner owner = InventoryOwner.Default;
	public GameObject[] inventoryBoxes = null;
	[SerializeField] private GameObject inventoryUI = null;

	void Start()
	{
		InventoryToolbox.instance.AddGlobalComponent(owner, this);
		SetBoxIDs();
		for(int x = inventoryBoxes.Length - 1; x >= inventory.size && x >= 0; x--)
		{
			inventoryBoxes[x].SetActive(false);
		}
		inventory.Setup();
		inventory.ResetCoins();
		inventoryUI.SetActive(false);
	}

	private void SetBoxIDs()
	{
		for(int x = 0; x < inventoryBoxes.Length; x++)
		{
			inventoryBoxes[x].GetComponent<InventoryBoxManager>().SetID(x);
		}
	}

	public void ManageCoins(int change)
	{
		inventory.ManageCoins(change);
		//coinsText.SetText("Coins: " + inventory.GetCoins());
		Debug.Log(inventory.GetCoins() + " coins in the blacksmith's inventory");
	}

	public int GetCoins()
	{
		return inventory.GetCoins();
	}

	public bool AddToInventory(Item item)
	{
		int index = inventory.Add(item);
		if(index > -1 && index <= inventory.size)
		{
			inventoryBoxes[index].GetComponent<InventoryBoxManager>().UpdateIcon(item.icon);
			return true;
		}
		return false;
	}

	public Inventory.InventoryInfo Remove(int slot)
	{
		return inventory.Remove(slot);
	}

	public Inventory.InventoryInfo Replace(int slot, Inventory.InventoryInfo info)
	{
		return inventory.Replace(slot, info);
	}

	public bool Swap(int slot1, int slot2)
	{
		return inventory.Swap(slot1, slot2);
	}

	public void ToggleInventory()
	{
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}

	public void ShowInventory()
	{
		inventoryUI.SetActive(true);
	}

	public void HideInventory()
	{
		inventoryUI.SetActive(false);
	}

}
