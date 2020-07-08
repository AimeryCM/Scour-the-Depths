using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "CharacterClass")]
public class CharacterClass : ScriptableObject
{
	//displayed
	public CharClass charClass;
	public int maxHealth = 0;
	public int attackPower = 0;
	public int magicPower = 0;
	[Range(0,1)] public float resistance = 0f;
	public int movespeed = 0;

	//hidden
	public int knockbackApplication = 0;
	[Range(0,1)] public float knockbackResistance = 0f;	//the percentage of knockback resisted
	public int jumpHeight = 0;
	public int doubleJumps = 0;
	public int dashCooldown = 0;	//0 means dashes disabled
	[Range(0,1)] public float dodgeChance = 0f;
}