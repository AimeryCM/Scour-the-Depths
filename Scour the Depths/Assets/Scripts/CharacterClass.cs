using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "CharacterClass")]
public class CharacterClass : ScriptableObject
{
	//displayed
	public int maxHealth = 0;
	public int attackPower = 0;
	public int magicPower = 0;
	[Range(0,1)] public float resistance = 0f;

	//hidden
	public float knockbackApplication = 1f;
	[Range(0,1)] public float knockbackResistance = 0f;	//the percentage of knockback resisted
	public float movespeed = 0f;
	public float jumpHeight = 0f;
	public int doubleJumps = 0;
	public float dashCooldown = 0f;	//0 means dashes disabled
	[Range(0,1)] public float dodgeChance = 0f;
}