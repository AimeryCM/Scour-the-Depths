using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	public static InputHandler instance;

	public GameObject character;
	public InputActionMap playerActions;
	public InputActionMap inventoryActions;
	public InputActionMap attackActions;

	private float horizontalMove = 0f;
	private MovementController moveControl;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;
		
		moveControl = character.GetComponent<MovementController>();
		inventoryActions["Inventory"].performed += ctx => Inventory.instance.ToggleInventory();
		playerActions["Jump"].performed += ctx => moveControl.Jump();
	}

	void Update()
	{
		horizontalMove = playerActions["Horizontal"].ReadValue<float>();
	}

	void FixedUpdate()
	{
		moveControl.Move(horizontalMove * Time.fixedDeltaTime);
	}

	void OnEnable()
	{
		playerActions.Enable();
		inventoryActions.Enable();
		attackActions.Enable();
	}

	void OnDisable()
	{
		playerActions.Disable();
		inventoryActions.Disable();
		attackActions.Disable();
	}
	
}
