﻿using UnityEngine;
using UnityEngine.InputSystem;

public class Attack_Brawler : MonoBehaviour
{

    public Animator animator = null;
    public Transform attackPos = null;
    public LayerMask enemyLayers = 0;
    public float attackRadius = 0;
    public int punchDamage = 0;

    void Start()
    {
        InputHandler.instance.attackActions["Punch"].performed += ctx => InitiatePunch();
    }

    void InitiatePunch()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Brawler_Punch"))
            animator.SetTrigger("PunchTrigger");
    }

    void PunchDamage()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemyLayers);
        foreach (Collider2D collider2D in enemiesToDamage)
        {
            collider2D.GetComponent<Enemy>().Damage(punchDamage, false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
