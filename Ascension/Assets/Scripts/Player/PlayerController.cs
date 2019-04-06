using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputManager im;
    private CinemachineTargetGroup tg;
    private PunchMechanic pm;
    public Animator anim;
    public StaticVariables.player svp;

    // Options:
    public bool startFacingLeft;
    public bool immoblie;
    public bool isWin = false;

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

    public float moveInput;
    private bool facingRight = true;


    // Jump Mechanic:
    [Range(0, 50)]
    public float jumpSpeed;
    public float jumpDelayTime;
    private float jumpTimer = 0f;
    private float jumpWait = 0.25f;

    public bool isGrounded;
    private bool isJump = false;
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
    public float wall_jump_delay;
    private bool canWallJump = true;

    // Knockback Mechanic:
    private bool isKnockedbacked = false;
    public float kb_duration;
    private float kb_timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        tg = GameObject.Find("TARGET_GROUP").GetComponent<CinemachineTargetGroup>();
        pm = GetComponent<PunchMechanic>();

        if (startFacingLeft)
        {
            Flip();
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckDimentions, 0, whatIsGround);
        isAgainstWall = Physics2D.OverlapBox(wallCheck.position, wallCheckDimentions, 0, whatIsWall);


        if (isGrounded && im.AisPushed(PlayerNum))
        {
            isJump = true;
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
        // if player is not knockedbacked or not immoblie:
        if (!isKnockedbacked && !immoblie)
        {
            // get move input
            moveInput = im.HorizontalMove(PlayerNum);

            // if player is not attacking
            if (!pm.isAttacking)
            {
                if (moveInput == 0f)
                {
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    anim.SetBool("isRunning", true);
                }

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
                    if (isAgainstWall && !isGrounded && im.AIsHeldDown(PlayerNum) && canWallJump)
                    {
                        rb.velocity = (new Vector2(rb.velocity.x, 0f));
                        rb.AddForce(new Vector2(0f, wallJumpForce));
                        canWallJump = false;
                        StartCoroutine(WallJumpDelay());
                    }


                    Flip();
                }



                // jump player
                if (isJump)
                {
                    // play jump animation:
                    if (!anim.GetBool("isJumping"))
                    {
                        anim.SetBool("isJumping", true);
                    }
                    StartCoroutine(JumpWithDelay());

                    isJump = false;
                }
                if (isJumping)
                {
                    jumpTimer += Time.deltaTime;
                    if (jumpTimer >= jumpWait)
                    {
                        if (isGrounded)
                        {
                            isJumping = false;
                            anim.SetBool("isJumping", false);
                            jumpTimer = 0f;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator WallJumpDelay()
    {
        yield return new WaitForSeconds(wall_jump_delay);
        // time until player can wall jump again
        canWallJump = true;
    }

    private IEnumerator JumpWithDelay()
    {
        yield return new WaitForSeconds(jumpDelayTime);
        //rb.AddForce(new Vector2(0f, jumpForce));
        rb.velocity = Vector2.up * jumpSpeed;
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
            anim.Play("Blue_PlayerKnockedDownAnim");
            rb.AddForce(new Vector2(0f, force));   
        }
    }

    public void Deactivate()
    {
        for(int i=0; i < tg.m_Targets.Length; i++)
        {
            if (tg.m_Targets[i].target.transform == this.transform)
            {
                tg.m_Targets[i].weight = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Activate()
    {
        for (int i = 0; i < tg.m_Targets.Length; i++)
        {
            if (tg.m_Targets[i].target.transform == this.transform)
            {
                tg.m_Targets[i].weight = 1;
                this.gameObject.SetActive(true);
            }
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
