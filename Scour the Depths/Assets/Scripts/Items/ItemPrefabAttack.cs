using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabAttack : MonoBehaviour
{
	public LayerMask enemyLayers = 0;
	public int comboAttackCount = 1;
	public Transform[] attackPoints = null;
	public float[] attackPointRadii = null;
	public int[] attackDamage = null;
	private Animator weaponAnimator = null;

	void Start()
	{
		weaponAnimator = gameObject.GetComponent<Animator>();
	}

	public void OnClick()
	{
		weaponAnimator.SetTrigger("AttackBasic");
	}

	public void damage(int attackNumber)
	{
		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoints[attackNumber].position, attackPointRadii[attackNumber], enemyLayers);
		foreach (Collider2D collider2D in enemiesToDamage)
		{
			collider2D.GetComponent<Enemy>().Damage(attackDamage[attackNumber], false);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		for(int x = 0; x < attackPoints.Length; x++)
		{
			Gizmos.DrawWireSphere(attackPoints[x].position, attackPointRadii[x]);
		}
	}
}
