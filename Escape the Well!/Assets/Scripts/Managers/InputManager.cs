using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool YisPushed(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("Y_" + playerNum.ToString());
    }

    public bool BisPushed(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("B_" + playerNum.ToString());
    }

    public bool AisPushed(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("A_" + playerNum.ToString());
    }

    public bool AIsHeldDown(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButton("A_" + playerNum.ToString());
    }

    public bool XisPushed(PlayerController.PlayerControllerNum playerNum)
    {
        return Input.GetButtonDown("X_" + playerNum.ToString());
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
