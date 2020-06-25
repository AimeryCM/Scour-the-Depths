using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int maxHealth = 0;
	public DropInfo dropInfo = null;
	public float dropXFactor = 0;
	public float dropYFactor = 0;
	public Animator animator = null;
	[SerializeField] private Transform damageTextSpawn = null;
	[SerializeField] private GameObject damageTextPrefab = null;
	private int currentHealth = 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

	public void Attack()
	{
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
		List<DropInfo.ItemQuantity> drops = dropInfo.GetDrops();
		foreach (DropInfo.ItemQuantity iq in drops)
		{
			Debug.Log("Dropping " + iq.quantity + " copies of " + iq.item.name);
			for(int x = 0; x < iq.quantity; x++)
			{
				GameObject gobj = Instantiate(iq.item, transform.position, Quaternion.identity);
				gobj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1 * dropXFactor, dropXFactor), Random.Range(0, dropYFactor)), ForceMode2D.Impulse);
			}
		}
		Debug.Log("Ow Oof Ouch my bones.");
		Destroy(this.gameObject);
	}
}
