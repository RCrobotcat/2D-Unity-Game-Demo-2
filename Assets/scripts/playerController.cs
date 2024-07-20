using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isGrounded; // Indicates if the player is on the ground
    bool doubleJumpUsed; // Indicates if the player has used double jump

    private float horizontal;

    private Vector2 lookDirection = new Vector2(1, 0);
    public float speed = 7f; // Adjustable move speed
    public float jumpForce = 7f; // Adjustable jump force

    private Rigidbody2D rigidbody2d;
    private Animator playerAnimator;
    private SpriteRenderer sprite;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ResetJump();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);

        // Set look direction based on movement
        if (!Mathf.Approximately(horizontal, 0.0f))
        {
            lookDirection.Set(horizontal, 0);
            lookDirection.Normalize();
        }

        playerAnimator.SetFloat("Speed", rigidbody2d.velocity.magnitude);

        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
        }

        HandleJump();

        /*Debug.Log(rigidbody2d.velocity.y);*/

        if (Mathf.Abs(rigidbody2d.velocity.y) > 0.1f)
        {
            playerAnimator.SetBool("isJumpOrFall", true);
            if (rigidbody2d.velocity.y > 0.1f)
            {
                playerAnimator.SetFloat("JumpOrFall", 1);
            }
            if (rigidbody2d.velocity.y < -0.1f)
            {
                playerAnimator.SetFloat("JumpOrFall", -1);
            }
        }
        else
        {
            playerAnimator.SetBool("isJumpOrFall", false);
            playerAnimator.SetFloat("JumpOrFall", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump mechanics when colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            ResetJump();
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || !doubleJumpUsed)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);

                if (!isGrounded)
                {
                    playerAnimator.SetTrigger("doubleJump");
                    doubleJumpUsed = true;
                }
                isGrounded = false;
            }
        }
    }

    private void ResetJump()
    {
        isGrounded = true;
        doubleJumpUsed = false;
    }
}
