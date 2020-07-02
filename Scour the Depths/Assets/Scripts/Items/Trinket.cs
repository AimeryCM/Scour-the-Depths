using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trinket", menuName = "Item/Trinket")]
public class Trinket : Item
{
	public AbilityManager.Ability ability = AbilityManager.Ability.Default;
}
