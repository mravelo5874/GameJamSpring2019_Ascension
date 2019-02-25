using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        PlayerController pc = player.GetComponentInParent<PlayerController>();

        if (pc != null)
        {
            Color color = player.GetComponentInParent<PlayerVisual>().color;
            string playerNum = pc.PlayerNum.ToString();

            pc.Deactivate();
            gm.PlayerComplete(color, playerNum);
        }
    }
}
