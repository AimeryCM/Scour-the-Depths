using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public struct PlayerStats
	{
		public CharacterClass charClass;
		public Item[] equippedItems;
		public LinkedList<Item> traits;
		public int[] upgrades;

		/*
		Upgrades indexes:
		0 = Health
		1 = AttackPower
		2 = MagicPower
		3 = Resistance
		4 = Movespeed
		*/

		public PlayerStats(CharacterClass character, Item[] items, LinkedList<Item> traitList, int[] ups)
		{
			charClass = character;
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
		}
	}

	public HealthBar healthBar = null;
	private CharacterClass charClass = null;
	private int maxHealth = 10;
	private int currentHealth = 0;
	private int attackPower = 0;
	private int magicPower = 0;
	private float resistance = 0f;
	private float movespeed = 1f;
	private float knockbackAmplification = 1f;
	private float knockbackResistance = 0f;
	private float jumpHeight = 1f;
	private int doubleJumps = 0;
	private float dashCooldown = 0f;
	private float dodgeChance = 0f;

	private int[] upgrades = new int[GlobalVariables.visibleTraitCount];

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void Setup(PlayerStats stats)
	{
		System.Array.Copy(stats.upgrades, upgrades, GlobalVariables.visibleTraitCount);
		maxHealth = stats.charClass.maxHealth + GlobalVariables.healthPerUpgrade * stats.upgrades[0];
		currentHealth = maxHealth;
		attackPower = stats.charClass.attackPower + GlobalVariables.attackPerUpgrade * stats.upgrades[1];
		magicPower = stats.charClass.magicPower + GlobalVariables.magicPerUpgrade * stats.upgrades[2];
		resistance = stats.charClass.resistance + GlobalVariables.resistancePerUpgrade * stats.upgrades[3];
		movespeed = stats.charClass.movespeed + GlobalVariables.movePerUpgrade * stats.upgrades[4];
		knockbackAmplification = stats.charClass.knockbackApplication;
		knockbackResistance = stats.charClass.knockbackResistance;
		jumpHeight = stats.charClass.jumpHeight;
		doubleJumps = stats.charClass.doubleJumps;
		dashCooldown = stats.charClass.dashCooldown;
		dodgeChance = stats.charClass.dodgeChance;
	}
}
