using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
	public HealthBar healthBar = null;
	public TextMeshProUGUI[] statText = null;
	private CharacterClassStats charClass = null;
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
		charClass = pStats.characterClass;

		maxHealth = new IntStat(pStats.characterClass.maxHealth);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.Health]; x++)
			maxHealth.AddModifier(GlobalVariables.healthPerUpgrade);
		currentHealth = maxHealth.GetStat();
		healthBar.SetMaxHealth(maxHealth.GetStat());

		attackPower = new IntStat(pStats.characterClass.attackPower);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.AttackPower]; x++)
			attackPower.AddModifier(GlobalVariables.attackPerUpgrade);

		magicPower = new IntStat(pStats.characterClass.magicPower);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.MagicPower]; x++)
			magicPower.AddModifier(GlobalVariables.magicPerUpgrade);
		
		resistance = new FloatStat(pStats.characterClass.resistance);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.Resistance]; x++)
			resistance.AddModifier(GlobalVariables.resistancePerUpgrade);
		
		moveSpeed = new IntStat(pStats.characterClass.movespeed);
		for(int x = 0; x < pStats.upgrades[(int)PlayerStats.UpgradeIndex.Movespeed]; x++)
			moveSpeed.AddModifier(GlobalVariables.movePerUpgrade);
		
		knockbackAmplification = new IntStat(pStats.characterClass.knockbackApplication);
		knockbackResistance = new FloatStat(pStats.characterClass.knockbackResistance);
		jumpHeight = new IntStat(pStats.characterClass.jumpHeight);
		doubleJumps = new IntStat(pStats.characterClass.doubleJumps);
		dashCooldown = new IntStat(pStats.characterClass.dashCooldown);
		dodgeChance = new FloatStat(pStats.characterClass.dodgeChance);

		for(int x = 0; x < GlobalVariables.trinketSlots; x++)
		{
			foreach(StatModifier statMod in ((Trinket)pStats.equippedItems[x + GlobalVariables.weaponSlots]).modifiers)
			{
				ApplyModifier(statMod.stat, statMod.percent, statMod.amount);
			}
		}
		
		UpdateStatText();
	}

	public void UpdateStatText()
	{
		statText[0].text = attackPower.GetStat().ToString();
		statText[1].text = magicPower.GetStat().ToString();
		statText[2].text = Mathf.FloorToInt(resistance.GetStat() * 100).ToString() + "%";
		statText[3].text = moveSpeed.GetStat().ToString();
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
