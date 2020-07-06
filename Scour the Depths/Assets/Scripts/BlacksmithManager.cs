using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
	public Animator animator = null;
	[SerializeField] private NPCInventoryManager inventory = null;
	public Item ironSword = null;
	public Trinket cloak = null;

	void Start()
	{
		PopulateInventory();
	}

	public void OnInteract()
	{
		animator.SetTrigger("Speak");
		inventory.ShowInventory();
	}

	public void OnLeave()
	{
		inventory.HideInventory();
	}

	public void PopulateInventory()
	{
		List<Inventory.InventoryInfo> items = new List<Inventory.InventoryInfo>();
		items.Add(new Inventory.InventoryInfo(ironSword, 1));
		items.Add(new Inventory.InventoryInfo(cloak, 1));
		Debug.Log("Populating inventory: " + items[0].ToString());
		inventory.Setup(items);
	}
}
