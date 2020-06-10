using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public MovementController controller;

	private float horizontalMove = 0f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
	}
}
