using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trinket", menuName = "Item/Trinket")]
public class Trinket : Item
{
	public AbilityInfo abilityInfo = new AbilityInfo(Ability.Default, 0f);
	public List<StatModifier> modifiers = null;
}