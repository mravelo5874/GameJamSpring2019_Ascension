using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicJumpMechanic : MonoBehaviour
{
    private PlayerController controller;
    private InputManager im;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        controller = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && im.Jump(controller.PlayerNum))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
