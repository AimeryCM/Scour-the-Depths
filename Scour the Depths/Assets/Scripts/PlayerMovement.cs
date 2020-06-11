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

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
		jump = playerActions["Jump"].triggered;
    }

	void FixedUpdate()
	{
		Debug.Log(jump);
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
	}
}
