using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

	[SerializeField] private float jumpForce;
	[Range(0,1)] [SerializeField] private float crouchSpeed;
	[SerializeField] private float acceleration;
	[SerializeField] private float maxSpeed;
	[SerializeField] private short jumpCount;
	private Rigidbody2D rbody;
	private Vector3 velocity = Vector3.zero;
	private bool facingRight = true;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

	public void Move(float move, bool crouch, bool jump)
	{
		//updates velocity vector of the player
		Vector3 targetVelocity = new Vector2(maxSpeed * move, rbody.velocity.y);
		rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, acceleration);
		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !facingRight)
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && facingRight)
			Flip();
	}

	private void Flip()
	{
		//flip our boolean
		facingRight = !facingRight;

		//multiply character scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
