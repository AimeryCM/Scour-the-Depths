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

		public enum UpgradeIndex
		{
			Health,
			AttackPower,
			MagicPower,
			Resistance,
			Movespeed
		}

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
	private IntStat maxHealth = null;
	private int currentHealth = 0;
	private IntStat attackPower = null;
	private IntStat magicPower = null;
	private FloatStat resistance = null;
	private IntStat moveSpeed = null;
	private IntStat knockbackAmplification = null;
	private FloatStat knockbackResistance = null;
	private IntStat jumpHeight = null;
	private IntStat doubleJumps = null;
	private IntStat dashCooldown = null;
	private FloatStat dodgeChance = null;

	private int[] upgrades = new int[GlobalVariables.visibleTraitCount];

	void Start()
	{
		healthBar.SetMaxHealth(1);
	}

	public void Setup(PlayerStats pStats)
	{
		System.Array.Copy(pStats.upgrades, upgrades, GlobalVariables.visibleTraitCount);
		charClass = pStats.charClass;

		maxHealth = new IntStat(pStats.charClass.maxHealth);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.Health]; x++)
			maxHealth.AddModifier(GlobalVariables.healthPerUpgrade);
		currentHealth = maxHealth.GetStat();
		healthBar.SetMaxHealth(maxHealth.GetStat());

		attackPower = new IntStat(pStats.charClass.attackPower);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.AttackPower]; x++)
			attackPower.AddModifier(GlobalVariables.attackPerUpgrade);

		magicPower = new IntStat(pStats.charClass.magicPower);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.MagicPower]; x++)
			magicPower.AddModifier(GlobalVariables.magicPerUpgrade);
		
		resistance = new FloatStat(pStats.charClass.resistance);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.Resistance]; x++)
			resistance.AddModifier(GlobalVariables.resistancePerUpgrade);
		
		moveSpeed = new IntStat(pStats.charClass.movespeed);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.Movespeed]; x++)
			moveSpeed.AddModifier(GlobalVariables.movePerUpgrade);
		
		knockbackAmplification = new IntStat(pStats.charClass.knockbackApplication);
		knockbackResistance = new FloatStat(pStats.charClass.knockbackResistance);
		jumpHeight = new IntStat(pStats.charClass.jumpHeight);
		doubleJumps = new IntStat(pStats.charClass.doubleJumps);
		dashCooldown = new IntStat(pStats.charClass.dashCooldown);
		dodgeChance = new FloatStat(pStats.charClass.dodgeChance);

		for(int x = 0; x < GlobalVariables.trinketSlots; x++)
		{
			foreach(StatModifier statMod in ((Trinket)pStats.equippedItems[x + GlobalVariables.weaponSlots]).modifiers)
			{
				ApplyModifier(statMod.stat, statMod.percent, statMod.amount);
			}
		}
	}

	public void ApplyModifier(CharacterStat stat, float mod, int quant)
	{
		switch(stat)
		{
			case CharacterStat.Health:
				if(mod != 0)
				{
					maxHealth.AddModifier(mod);
				}
				else if(quant != 0)
				{
					maxHealth.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				healthBar.SetMaxHealth(maxHealth.GetStat());
				break;
			case CharacterStat.Attack:
				if(mod != 0)
				{
					attackPower.AddModifier(mod);
				}
				else if(quant != 0)
				{
					attackPower.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.Magic:
				if(mod != 0)
				{
					magicPower.AddModifier(mod);
				}
				else if(quant != 0)
				{
					magicPower.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.Resistance:
				if(mod != 0)
				{
					resistance.AddModifier(mod);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.Move:
				if(mod != 0)
				{
					moveSpeed.AddModifier(mod);
				}
				else if(quant != 0)
				{
					moveSpeed.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.KnockbackAmplification:
				if(mod != 0)
				{
					knockbackAmplification.AddModifier(mod);
				}
				else if(quant != 0)
				{
					knockbackAmplification.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.KnockbackResistance:
				if(mod != 0)
				{
					knockbackResistance.AddModifier(mod);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.JumpHeight:
				if(mod != 0)
				{
					jumpHeight.AddModifier(mod);
				}
				else if(quant != 0)
				{
					jumpHeight.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.DoubleJumps:
				if(mod != 0)
				{
					doubleJumps.AddModifier(mod);
				}
				else if(quant != 0)
				{
					doubleJumps.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.DashCooldown:
				if(mod != 0)
				{
					dashCooldown.AddModifier(mod);
				}
				else if(quant != 0)
				{
					dashCooldown.AddModifier(quant);
				}
				else
					Debug.Log("No stat change applied");
				break;
			case CharacterStat.DodgeChance:
				if(mod != 0)
				{
					dodgeChance.AddModifier(mod);
				}
				else
					Debug.Log("No stat change applied");
				break;
			default:
				break;
		}
	}
}
