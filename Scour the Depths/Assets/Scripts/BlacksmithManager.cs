using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
	public Animator animator;

	public void OnInteract()
	{
		animator.SetTrigger("Speak");
	}
}
