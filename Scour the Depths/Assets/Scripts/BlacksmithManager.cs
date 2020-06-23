using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
	public Animator animator = null;
	public Inventory inventory = null;

	public void OnInteract()
	{
		animator.SetTrigger("Speak");
	}
}
