using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
	public ItemType itemType = ItemType.Material;
	public string itemName = "New Item";
	public Sprite icon = null;

}

public enum ItemType {Weapon, Coin, Material, Consumable, Ability}