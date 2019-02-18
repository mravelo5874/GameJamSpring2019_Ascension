using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputManager im;
    public Animator anim;

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
    public Vector2 groundCheckDimentions;
    public LayerMask whatIsGround;


    // Wall Jump Mechanic:
    [Range(0, 1000)]
    public float wallJumpForce;

    private bool isAgainstWall;
    public Transform wallCheck;
    public Vector2 wallCheckDimentions;
    public LayerMask whatIsWall;

    // Knockback Mechanic:
    private bool isKnockedbacked = false;
    public float kb_duration;
    private float kb_timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckDimentions, 0, whatIsGround);
        isAgainstWall = Physics2D.OverlapBox(wallCheck.position, wallCheckDimentions, 0, whatIsWall);


        if (isGrounded && im.Jump(PlayerNum))
        {
            isJumping = true;
        }

        if (isKnockedbacked)
        {
            kb_timer += Time.deltaTime;
            if (kb_timer >= kb_duration)
            {
                anim.SetBool("isKnockedDown", false);
                isKnockedbacked = false;
                kb_timer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        // if player is not knockedbacked:
        if (!isKnockedbacked)
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
                // wall jump mechanic:
                if (isAgainstWall && !isGrounded && im.JumpIsPushed(PlayerNum))
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
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Knockback(float force)
    {
        if (!isKnockedbacked)
        {
            isKnockedbacked = true;
            anim.SetBool("isKnockedDown", true);
            anim.Play("PlayerKnockedDownAnimation");
            rb.AddForce(new Vector2(0f, force));   
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckDimentions);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheck.position, wallCheckDimentions);

    }
}
