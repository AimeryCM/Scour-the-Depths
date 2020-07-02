using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
	public enum Ability
	{
		Default,
		Invisibility,
		Summon
	}

	public void UseAbility(Ability ability, float duration)
	{
		switch (ability)
		{
			case Ability.Invisibility:
				break;
			default:
				break;
		}
	}
}