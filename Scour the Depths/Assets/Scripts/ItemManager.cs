using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public Item item;
	protected SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = item.icon;
		tag = "Item";
	}

	public void Pickup()
	{
		Debug.Log("Picked up " + item.itemName);
		Destroy(gameObject);
	}
}
