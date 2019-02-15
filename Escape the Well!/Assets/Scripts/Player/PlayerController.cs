using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputManager im;

    public enum PlayerControllerNum
    {
        P1,
        P2,
        P3,
        P4
    };

    public PlayerController.PlayerControllerNum PlayerNum;

    // Horizontal Movement:
    [Range(0, 100)]
    public float horizontalSpeed_ground;
    [Range(0, 100)]
    public float horizontalSpeed_air;

    private float moveInput;
    private bool facingRight = true;


    // Jump Mechanic:
    [Range(0, 50)]
    public float jumpSpeed;

    private bool isGrounded;
    private bool isJumping = false;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;


    // Wall Jump Mechanic:
    [Range(0, 1000)]
    public float wallJumpForce;

    private bool isAgainstWall;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isAgainstWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);


        if (isGrounded && im.Jump(PlayerNum))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        // get move input
        moveInput = im.HorizontalMove(PlayerNum);
    
        // move player horizontally
        if (isGrounded)
        {
            rb.velocity = new Vector2(moveInput * horizontalSpeed_ground, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput * horizontalSpeed_air, rb.velocity.y);
        }


        if (!facingRight && moveInput > 0 || facingRight == true && moveInput < 0)
        {
            if (isAgainstWall && !isGrounded)
            {
                rb.velocity = (new Vector2(rb.velocity.x, 0f));
                rb.AddForce(new Vector2(0f, wallJumpForce));
            }


            Flip();
        }

        

        // jump player
        if (isJumping)
        {
            //rb.AddForce(new Vector2(0f, jumpForce));
            rb.velocity = Vector2.up * jumpSpeed;
            isJumping = false;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
