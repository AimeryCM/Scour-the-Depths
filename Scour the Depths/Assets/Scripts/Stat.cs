using System.Collections.Generic;
using UnityEngine;

public class IntStat
{
	public int amount = 0;
	private List<int> numMods = null;
	private List<float> percentMods = null;

	public IntStat(int num)
	{
		amount = num;
		numMods = new List<int>();
		percentMods = new List<float>();
	}

	public void AddModifier(int mod)
	{
		numMods.Add(mod);
	}

	public void AddModifier(float mod)
	{
		percentMods.Add(mod);
	}

	public bool RemoveModifier(int mod)
	{
		return numMods.Remove(mod);
	}

	public bool RemoveModifier(float mod)
	{
		return percentMods.Remove(mod);
	}

	public int GetStat()
	{
		int result = amount;
		foreach(int modifier in numMods)
		{
			result += modifier;
		}
		float percMod = 0;
		foreach(float modifier in percentMods)
		{
			percMod += modifier;
		}
		return Mathf.FloorToInt(result + (result * percMod));
	}
}

public class FloatStat
{
	public float percent = 0;
	private List<float> mods = null;

	public FloatStat(float num)
	{
		percent = num;
		mods = new List<float>();
	}

	public void AddModifier(float mod)
	{
		mods.Add(mod);
	}

	public bool RemoveModifier(float mod)
	{
		return mods.Remove(mod);
	}

	//uses multiplicitave so that the value approaches 100% but won't reach
	public float GetStat()
	{
		float result = 1 - percent;
		foreach(float mod in mods)
		{
			result *= 1 - mod;
		}
		return 1 - result;
	}
}