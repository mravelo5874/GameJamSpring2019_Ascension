using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMechanic : MonoBehaviour
{
    private PlayerController controller;
    private InputManager im;
    public Animator anim;

    private bool isPunching = false;
    public float punch_duration;
    private float punch_timer;

    public Transform punchPos;
    public LayerMask whatIsPlayer;
    public Vector2 punchBoxSize;
    [Range(0, 2000)]
    public float knockbackForce;

    private void Awake()
    {
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!isPunching)
        {
            // able to punch
            if (im.Punch(controller.PlayerNum))
            {
                isPunching = true;
                anim.Play("PlayerPunchAnimation");
                
                Collider2D[] playersToPunch = Physics2D.OverlapBoxAll(punchPos.position, punchBoxSize, 0, whatIsPlayer);
                for (int i = 0; i < playersToPunch.Length; i++)
                {
                    playersToPunch[i].GetComponent<PlayerController>().Knockback(knockbackForce);
                }
            }     
        }
        else
        {
            punch_timer += Time.deltaTime;
            if (punch_timer >= punch_duration)
            {
                isPunching = false;
                punch_timer = 0f;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(punchPos.position, punchBoxSize);
    }
}
