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
    }
}
