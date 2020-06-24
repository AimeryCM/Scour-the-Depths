using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public Item item = null;
	protected SpriteRenderer spriteRenderer = null;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = item.icon;
		tag = "Item";
	}

	public void Pickup()
	{
		Debug.Log("Picked up " + item.itemName);
		switch(item.itemType)
		{
			case ItemType.Coin:
				InventoryToolbox.instance.GetGlobalComponent(InventoryOwner.Player).ManageCoins(1);
				Destroy(gameObject);
				break;
			default:
				if(InventoryToolbox.instance.GetGlobalComponent(InventoryOwner.Player).AddToInventory(item))
				{
					Debug.Log("Added " + item.itemName + " to the inventory");
					Destroy(gameObject);
				}
				break;
		}
	}
}
