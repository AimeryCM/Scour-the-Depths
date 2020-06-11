using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

	public MovementController controller;
	public InputActionMap playerActions;

	private float horizontalMove = 0f;
	private bool jump = false;

	void Awake()
	{
		playerActions["Jump"].performed += ctx => controller.Jump();
		//playerActions["Horizontal"].started += ctx => controller.Move(ctx.ReadValue<float>());
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
