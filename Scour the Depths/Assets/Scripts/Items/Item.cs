﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Default Item")]
public class Item : ScriptableObject
{
	public ItemType itemType = ItemType.Material;
	public string itemName = "New Item";
	public Sprite icon = null;
	public int cost = 0;
}

public enum ItemType {Weapon, Coin, Material, Consumable, Trinket}