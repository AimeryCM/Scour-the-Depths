using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : ItemManager
{
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = item.icon;
		tag = "Coin";
	}

	public new void Pickup()
	{
		Inventory.instance.ManageCoins(1);
		Destroy(gameObject);
	}
}