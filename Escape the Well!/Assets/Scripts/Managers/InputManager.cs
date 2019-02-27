using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool BisPushed(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("B_" + playerNum.ToString());
    }

    public bool Jump(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("Jump_" + playerNum.ToString());
    }

    public bool JumpIsPushed(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButton("Jump_" + playerNum.ToString());
    }

    public bool Punch(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("Punch_" + playerNum.ToString());
    }

    public float HorizontalMove(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetAxisRaw("Horizontal_" + playerNum.ToString());
    }

    public float VerticalMove(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetAxisRaw("Vertical_" + playerNum.ToString());
    }

}
