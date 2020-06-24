using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
	public Animator animator = null;
	[SerializeField] private NPCInventoryManager inventory = null;

	public void OnInteract()
	{
		animator.SetTrigger("Speak");
		inventory.ShowInventory();
	}

	public void OnLeave()
	{
		inventory.HideInventory();
	}
}
