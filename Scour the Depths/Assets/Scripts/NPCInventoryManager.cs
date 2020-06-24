using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventoryManager : MonoBehaviour, IInventoryManager
{
	public Inventory inventory = null;
	public InventoryOwner owner = InventoryOwner.Default;
	public GameObject[] inventoryBoxes = null;
	public float markup = 1f;
	[SerializeField] private GameObject inventoryUI = null;

	void Start()
	{
		InventoryToolbox.instance.AddGlobalComponent(owner, this);
		SetBoxIDs();
		for(int x = inventoryBoxes.Length - 1; x >= inventory.size && x >= 0; x--)
		{
			inventoryBoxes[x].SetActive(false);
		}
		inventory.ResetCoins();
		inventoryUI.SetActive(false);
	}

	public void Setup(List<Inventory.InventoryInfo> items)
	{
		if(items == null)
			inventory.Setup();
		else
			inventory.Setup(items);
		UpdateSprites();
	}

	private void UpdateSprites()
	{
		for(int x = 0; x < inventory.size; x++)
		{
			if(inventory.GetItem(x).occupied)
				inventoryBoxes[x].GetComponent<InventoryBoxManager>().UpdateIcon(inventory.GetItem(x).item.icon);
			else
				inventoryBoxes[x].GetComponent<InventoryBoxManager>().SetIconToDefault();
		}
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

	public int GetCost(int index)
	{
		return Mathf.FloorToInt(inventory.GetItem(index).item.cost * markup);
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

	public float Markup()
	{
		return markup;
	}

	public void ToggleInventory()
	{
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}

	public void ShowInventory()
	{
		inventoryUI.SetActive(true);
		PrintInventory();
	}

	public void HideInventory()
	{
		inventoryUI.SetActive(false);
	}

	private void PrintInventory()
	{
		Debug.Log(owner + " Inventory:\n" + inventory.ToString());
	}

}
