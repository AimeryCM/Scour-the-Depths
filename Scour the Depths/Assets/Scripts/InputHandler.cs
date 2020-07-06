using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	public static InputHandler instance = null;

	public GameObject character = null;
	public InputActionMap playerActions = null;
	public InputActionMap inventoryActions = null;
	public InputActionMap attackActions = null;
	public InputActionMap abilityActions = null;

	private float horizontalMove = 0f;
	private MovementController moveControl = null;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;
	}

	void Start()
	{
		moveControl = character.GetComponent<MovementController>();
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
