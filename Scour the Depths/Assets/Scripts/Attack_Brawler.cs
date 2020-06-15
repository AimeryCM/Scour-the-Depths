using UnityEngine;
using UnityEngine.InputSystem;

public class Attack_Brawler : MonoBehaviour
{

    public InputActionMap attackActions;
    public Animator animator;
    public Transform attackPos;
    public LayerMask enemyLayers;
    public float attackRadius;
    public int punchDamage;

    void Awake()
    {
        attackActions["Punch"].performed += ctx => InitiatePunch();
    }

    void OnEnable()
    {
        attackActions.Enable();
    }

    void OnDisable()
    {
        attackActions.Disable();
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
