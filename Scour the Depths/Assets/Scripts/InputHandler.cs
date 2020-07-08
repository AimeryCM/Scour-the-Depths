using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	public static InputHandler instance = null;
	
	public InputActionMap playerActions = null;
	public InputActionMap inventoryActions = null;
	public InputActionMap attackActions = null;
	public InputActionMap abilityActions = null;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;
	}

	void OnEnable()
	{
		playerActions.Enable();
		inventoryActions.Enable();
		attackActions.Enable();
		abilityActions.Enable();
	}

	void OnDisable()
	{
		playerActions.Disable();
		inventoryActions.Disable();
		attackActions.Disable();
		abilityActions.Disable();
	}
	
}
