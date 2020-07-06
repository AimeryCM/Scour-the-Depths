using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Weapon")]
public class Weapon : Item
{
	[System.Serializable]
	public struct DamageOverTime
	{
		public float damagePerTick;
		public float timeBetweenTicks;
		public float duration;
		public OvertimeType type;

		public DamageOverTime(float damage, float spacing, float time, OvertimeType tp)
		{
			damagePerTick = damage;
			timeBetweenTicks = spacing;
			duration = time;
			type = tp;
		}

		public enum OvertimeType {Fire, Ice, Acid, Default}
	}

	public GameObject weaponPrefab = null;

	public WeaponType weaponType = WeaponType.Default;
	public DamageType damageType = DamageType.Physical;
	public float damage = 1f;
	public float knockback = 0f;
	public string description = "";
	public DamageOverTime dot = new DamageOverTime(0f, 1f, 0f, DamageOverTime.OvertimeType.Default);
	//a modifier to the animation speed
	public float speed = 1f;

	//something needed for when the item is held, whether it is a sprite or an animator

}

public enum WeaponType {Heavy, Light, Pierce, Ranged, Thrown, Default}
public enum DamageType {Physical, Magical}