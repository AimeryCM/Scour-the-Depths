using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

	[SerializeField] private float jumpHeight;
	[Range(0,1)] [SerializeField] private float crouchSpeed;
	[SerializeField] private float acceleration;
	[SerializeField] private float maxSpeed;
	[SerializeField] private short maxJumps;
    [SerializeField] private float boxRayDepth;
    [SerializeField] private float boxRayWidth;
	[SerializeField] private LayerMask groundLayerMask;
	
	private Rigidbody2D playerRigidBody;
	private Vector3 velocity = Vector3.zero;
	private bool facingRight = true;
	private bool grounded;
	private float jumpForce;
	private short jumps;
    private CircleCollider2D circleCollider;


    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
		jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
    }

	private void FixedUpdate()
	{
		grounded = IsGrounded();
	}

	public void Move(float move)
	{
		//updates velocity vector of the player
		Vector3 targetVelocity = new Vector2(maxSpeed * move, playerRigidBody.velocity.y);
		playerRigidBody.velocity = Vector3.SmoothDamp(playerRigidBody.velocity, targetVelocity, ref velocity, acceleration);
		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !facingRight)
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && facingRight)
			Flip();
	}

    public void Jump()
    {
		if(grounded)
			jumps = 0;
		if(jumps < maxJumps)
		{
			playerRigidBody.AddForce(Vector2.up * jumpForce * playerRigidBody.mass, ForceMode2D.Impulse);
			jumps++;
		}
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

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, new Vector2(boxRayWidth, boxRayDepth), 0f, Vector2.down, boxRayDepth, groundLayerMask);
		return raycastHit.collider != null;
    }

	private float CalculateJumpForce(float gravityStrength, float height){
		return Mathf.Sqrt(2 * gravityStrength * height);
	}
}
