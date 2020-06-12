using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

	public MovementController controller;
	public InputActionMap playerActions;

	private float horizontalMove = 0f;

	void Awake()
	{
		playerActions["Jump"].performed += ctx => controller.Jump();
	}

	void Update()
	{
		horizontalMove = playerActions["Horizontal"].ReadValue<float>();
	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime);
	}

	void OnEnable()
	{
		playerActions.Enable();
	}

	void OnDisable()
	{
		playerActions.Disable();
	}
}
