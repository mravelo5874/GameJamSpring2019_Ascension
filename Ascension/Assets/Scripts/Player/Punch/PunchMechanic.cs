using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMechanic : MonoBehaviour
{
    private PlayerController controller;
    private InputManager im;
    private PlayerAnimationBox pab;
    public Animator anim;

    public bool isAttacking = false;

    public float upAttackDuration;
    private bool isAttackUp = false;

    public float punchAttackDuration;
    private bool isAttackpunch = false;

    public float downAttackDuration;
    private bool isAttackDown = false;

    private float attack_timer = 0f;

    [Range(0, 2000)]
    public float knockbackForce;

    private void Awake()
    {
        pab = GetComponent<PlayerAnimationBox>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            // able to punch

            // if player is not moving and is on the ground
            if (controller.moveInput == 0f && controller.isGrounded) // Grounded Attacks
            {
                if (im.XisPushed(controller.PlayerNum))
                {
                    anim.Play(pab.horizontal_attack);
                    isAttacking = true;
                    isAttackpunch = true;
                    controller.pam.play_punch();
                }
                else if (im.YisPushed(controller.PlayerNum))
                {
                    anim.Play(pab.up_attack);
                    isAttacking = true;
                    isAttackUp = true;
                    controller.pam.play_punch();
                }
                else if (im.BisPushed(controller.PlayerNum))
                {
                    anim.Play(pab.down_attack);
                    isAttacking = true;
                    isAttackDown = true;
                    controller.pam.play_punch();
                }
            }
            else if (!controller.isGrounded) // Airial Attacks
            {
                if (im.XisPushed(controller.PlayerNum))
                {
                    anim.Play(pab.aerial_horizontal_attack);
                    isAttacking = true;
                    isAttackpunch = true;
                    controller.pam.play_punch();
                }
                else if (im.YisPushed(controller.PlayerNum))
                {
                    anim.Play(pab.aerial_up_attack);
                    isAttacking = true;
                    isAttackUp = true;
                    controller.pam.play_punch();
                }
                else if (im.BisPushed(controller.PlayerNum))
                {
                    anim.Play(pab.aerial_down_attack);
                    isAttacking = true;
                    isAttackDown = true;
                    controller.pam.play_punch();
                }
            }
        }
        else
        {
            if (isAttackUp)
            {
                attack_timer += Time.deltaTime;
                if (attack_timer >= upAttackDuration)
                {
                    isAttacking = false;
                    isAttackUp = false;
                    attack_timer = 0f;
                }
            }
            else if (isAttackpunch)
            {
                attack_timer += Time.deltaTime;
                if (attack_timer >= punchAttackDuration)
                {
                    isAttacking = false;
                    isAttackpunch = false;
                    attack_timer = 0f;
                }
            }
            else if (isAttackDown)
            {
                attack_timer += Time.deltaTime;
                if (attack_timer >= downAttackDuration)
                {
                    isAttacking = false;
                    isAttackDown = false;
                    attack_timer = 0f;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
    }
}
