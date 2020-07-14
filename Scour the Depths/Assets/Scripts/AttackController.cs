using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
	private int comboCounter = 0;
	private bool nextAttack = false;
	private PlayerInventoryManager playerInventoryManager = null;

	void Start()
	{
		InputHandler.instance.attackActions["Primary"].performed += ctx => OnAttack();
		playerInventoryManager = gameObject.GetComponent<PlayerInventoryManager>();
	}

	void Update()
	{

	}

	private void OnAttack()
	{
		GameObject temp = playerInventoryManager.GetWeaponObject(0);
		if(temp != null)
		{
			temp.GetComponent<ItemPrefabAttack>().OnClick();
		}
	}
}
