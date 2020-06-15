using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int maxHealth;
	[SerializeField] private Transform damageTextSpawn;
	[SerializeField] private GameObject damageTextPrefab;
	public GameObject damageText;
	private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int amount, bool crit)
    {
		currentHealth -= amount;
		Instantiate(damageTextPrefab, damageTextSpawn.position, Quaternion.identity).GetComponent<DamageTextScript>().Setup(amount, crit);
		if (currentHealth <= 0)
			Die();
    }

	private void Die()
	{
		Debug.Log("Ow Oof Ouch my bones.");
		Destroy(this.gameObject);
	}
}
