using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int maxHealth;
	private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(short amount)
    {
		currentHealth -= amount;
		if (currentHealth <= 0)
			Die();
    }

	private void Die()
	{
		Debug.Log("Ow Oof Ouch my bones.");
		Destroy(this.gameObject);
	}
}
