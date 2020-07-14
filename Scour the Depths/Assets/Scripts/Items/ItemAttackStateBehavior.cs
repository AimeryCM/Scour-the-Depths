﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackStateBehavior : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("ContinueAttack", false);
	}
}
