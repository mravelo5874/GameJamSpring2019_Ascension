using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBox : MonoBehaviour
{
    public PunchHitBox playerHitBox;
    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PunchHitBox hitBox = other.GetComponent<PunchHitBox>();

        if (hitBox != null && hitBox != playerHitBox)
        {
            playerController.Knockback(hitBox.punchMechanic.knockbackForce);
        }
    }
}
