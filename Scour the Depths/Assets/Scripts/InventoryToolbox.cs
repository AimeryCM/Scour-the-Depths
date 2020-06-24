using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToolbox : MonoBehaviour
{
    public static InventoryToolbox instance = null;
	private Dictionary<InventoryOwner, IInventoryManager> components = new Dictionary<InventoryOwner, IInventoryManager>();

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;
	}

	public bool AddGlobalComponent(InventoryOwner inventoryOwner, IInventoryManager inventoryManager)
	{
		if(components.ContainsKey(inventoryOwner))
		{
			Debug.LogWarning("Attempting to add multiple " + inventoryOwner.ToString() + " to the Inventory Toolbox!");
			return false;
		}
		if(inventoryOwner == InventoryOwner.Default)
		{
			Debug.LogWarning("Attempting to add a default inventory to the Inventory Toolbox!");
			return false;
		}
		components.Add(inventoryOwner, inventoryManager);
		return true;
	}

	public IInventoryManager GetGlobalComponent(InventoryOwner inventoryOwner)
	{
		IInventoryManager result;
		if(components.TryGetValue(inventoryOwner, out result))
		{
			return result;
		}
		return null;
	}
}

public enum InventoryOwner {Player, Blacksmith, Default}