using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

	[SerializeField] private float jumpHeight = 0;
	//[Range(0,1)] [SerializeField] private float crouchSpeed = 0;
	[SerializeField] private float acceleration = 0;
	[SerializeField] private float airAcceleration = 0;
	[SerializeField] private float maxSpeed = 0;
	[SerializeField] private short maxJumps = 0;
    [SerializeField] private float boxRayWidth = 0;
	[SerializeField] private float boxRayDistance = 0;
	[SerializeField] private LayerMask groundLayerMask = 0;
	public Animator animator = null;
	
	private Rigidbody2D playerRigidBody = null;
	private Vector3 velocity = Vector3.zero;
	private bool facingRight = true;
	private bool grounded = false;
	private float jumpForce = 0;
	private short jumps = 1;
    private CircleCollider2D circleCollider = null;


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
		jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
    }

	private void FixedUpdate()
	{
		animator.SetBool("Grounded", grounded = IsGrounded());
		animator.SetFloat("XSpeed", Mathf.Abs(playerRigidBody.velocity.x));
		animator.SetFloat("YSpeed", playerRigidBody.velocity.y);
	}

	public void Move(float move)
	{
        Vector3 targetVelocity = new Vector2(maxSpeed * move, playerRigidBody.velocity.y);
        if(grounded)
			playerRigidBody.velocity = Vector3.SmoothDamp(playerRigidBody.velocity, targetVelocity, ref velocity, acceleration);
        else
			playerRigidBody.velocity = Vector3.SmoothDamp(playerRigidBody.velocity, targetVelocity, ref velocity, airAcceleration);
		
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
            animator.SetTrigger("Jump");
			playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
			playerRigidBody.AddForce(Vector2.up * jumpForce * playerRigidBody.mass, ForceMode2D.Impulse);
			jumps++;
		}
    }

	private void Flip()
	{
		//flip our boolean
		facingRight = !facingRight;

		//multiply character scale by -1
		transform.Rotate(0f, 180f, 0f);
	}

    private bool IsGrounded()
    {
		//Debug.DrawRay(circleCollider.bounds.center, Vector3.down * (circleCollider.bounds.extents.y + boxRayDistance), Color.green);
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, new Vector2(boxRayWidth, circleCollider.bounds.extents.y), 0f, Vector2.down, boxRayDistance, groundLayerMask);
		//Debug.Log(raycastHit.collider);
		return raycastHit.collider != null;
    }

	private float CalculateJumpForce(float gravityStrength, float height){
		return Mathf.Sqrt(2 * gravityStrength * height);
	}
}
