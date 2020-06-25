using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public HealthBar healthBar = null;
	public int maxHealth = 10;
	private int currentHealth = 0;

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

    void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Item")
		{
			collision.gameObject.GetComponent<ItemManager>().Pickup();
		}
	}
}
