using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
	
	private int coins = 0;
	[SerializeField] private short inventorySize = 16;
	private List<Item> itemList;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;

		itemList = new List<Item>();
	}

	public void ManageCoins(int change)
	{
		coins = Mathf.Clamp(coins + change, 0, int.MaxValue);
		Debug.Log(coins + " coins in the inventory");
	}

	public int GetCoins()
	{
		return coins;
	}

	public bool AddToInventory(Item item)
	{
		if(itemList.Count >= inventorySize)
			return false;
		itemList.Add(item);
		return true;
	}
}
