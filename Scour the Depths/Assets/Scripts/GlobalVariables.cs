using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
	public static int trinketSlots = 4;
	public static int weaponSlots = 2;
	public static int hotbarSlots = 4;
	public static int playerInventorySlots = 16;
	public static int totalPlayerInventorySlots = trinketSlots + weaponSlots + hotbarSlots + playerInventorySlots;
	public static int visibleTraitCount = 5;
	public static int healthPerUpgrade = 1;
	public static int attackPerUpgrade = 1;
	public static int magicPerUpgrade = 1;
	public static float resistancePerUpgrade = 0.1f;
	public static int movePerUpgrade = 1;
}


public enum Ability{Default, Invisibility, Summon}

public enum CharacterStat {Health, Attack, Magic, Resistance, Move, KnockbackAmplification, KnockbackResistance, JumpHeight, DoubleJumps, DashCooldown, DodgeChance}

public enum CharClass {Nerd, Default}

[System.Serializable]
public struct StatModifier
{
	public CharacterStat stat;
	public float percent {get; private set;}
	public int amount {get; private set;}

	public StatModifier(CharacterStat statMod, float f)
	{
		stat = statMod;
		percent = f;
		amount = 0;
	}

	public StatModifier(CharacterStat statMod, int num)
	{
		stat = statMod;
		percent = 0;
		amount = num;
	}
}

[System.Serializable]
public struct AbilityInfo
{
	public Ability type;
	public float duration;
	public GameObject summon;

	public AbilityInfo(Ability name, float length)
	{
		type = name;
		duration = length;
		summon = null;
	}
}

public struct PlayerStats
{
	public CharacterClassStats characterClass;
	public Item[] equippedItems;
	public LinkedList<Item> traits;
	public int[] upgrades;
	public string name;
	
	public enum UpgradeIndex
	{
		Health,
		AttackPower,
		MagicPower,
		Resistance,
		Movespeed
	}
	
	public PlayerStats(CharacterClassStats character, Item[] items, LinkedList<Item> traitList, int[] ups)
	{
		characterClass = character;
		equippedItems = new Item[GlobalVariables.trinketSlots + GlobalVariables.weaponSlots];
		if(items.Length > 6)
			Debug.LogWarning("Attempting to make a PlayerStats struct with more than 6 equipped items");
		for(int x = 0; x < equippedItems.Length && x < items.Length; x++)
		{
			if(x < 2 && items[x].itemType == ItemType.Weapon)
				equippedItems[x] = items[x];
			if(x >= 2 && items[x].itemType == ItemType.Trinket)
				equippedItems[x] = items[x];
		}
		traits = new LinkedList<Item>();
		foreach(Item trait in traitList)
		{
			traits.AddLast(trait);
		}
		upgrades = new int[GlobalVariables.visibleTraitCount];
		System.Array.Copy(ups, upgrades, upgrades.Length);
		//maybe add a list and randomly chose one
		name = "Steve";
	}

	public PlayerStats(CharacterClassStats character, Item[] items1, LinkedList<Item> traitList, int[] ups, string name) : this(character, items1, traitList, ups)
	{
		this.name = name;
	}
}