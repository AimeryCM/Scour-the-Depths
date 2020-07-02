using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
	private Weapon[] weapons = null;
	private Trinket[] trinkets = null;

	public override void Setup()
	{
		base.Setup();
		weapons = new Weapon[GlobalVariables.weaponSlots];
		trinkets = new Trinket[GlobalVariables.trinketSlots];
	}

	public bool AddWeapon(Weapon weap)
	{
		for(int x = 0; x < weapons.Length; x++)
		{
			if(weapons[x] == null)
			{
				weapons[x] = weap;
				return true;
			}
		}
		return false;
	}

	public bool AddWeapon(Weapon weap, int index)
	{
		if(index < weapons.Length && weapons[index] == null)
		{
			weapons[index] = weap;
			return true;
		}
		return false;
	}

	public Weapon GetWeapon(int index)
	{
		if(index >= 0 && index < weapons.Length)
			return weapons[index];
		return null;
	}

	public Weapon RemoveWeapon(int index)
	{
		if(index < weapons.Length && index <= 0)
		{
			Weapon result = weapons[index];
			weapons[index] = null;
			return result;
		}
		return null;
	}

	public Weapon ReplaceWeapon(int index, Weapon weap)
	{
		Weapon result = RemoveWeapon(index);
		if(result != null)
		{
			AddWeapon(weap, index);
		}
		return result;
	}

	public bool AddTrinket(Trinket trin)
	{
		for(int x = 0; x < trinkets.Length; x++)
		{
			if(trinkets[x] == null)
			{
				trinkets[x] = trin;
				return true;
			}
		}
		return false;
	}

	public bool AddTrinket(Trinket trin, int index)
	{
		if(index < trinkets.Length && trinkets[index] == null)
		{
			trinkets[index] = trin;
			return true;
		}
		return false;
	}

	public Trinket GetTrinket(int index)
	{
		if(index >= 0 && index < trinkets.Length)
			return trinkets[index];
		return null;
	}

	public Trinket RemoveTrinket(int index)
	{
		if(index < trinkets.Length && index <= 0)
		{
			Trinket result = trinkets[index];
			trinkets[index] = null;
			return result;
		}
		return null;
	}

	public Trinket ReplaceTrinket(int index, Trinket trinket)
	{
		Trinket result = RemoveTrinket(index);
		if(result != null)
		{
			AddTrinket(trinket, index);
		}
		return result;
	}
}
