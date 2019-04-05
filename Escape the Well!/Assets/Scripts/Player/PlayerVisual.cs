using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public enum PlayerColor { Blue, Red, Green, Yellow };

    public PlayerVisual.PlayerColor playerColor;
    public Color color;

    private void Awake()
    {
        SetPlayerColor();
    }

    private void SetPlayerColor()
    {
        if (playerColor == PlayerVisual.PlayerColor.Red)
        {
            color = Color.red;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Blue)
        {
            color = Color.blue;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Green)
        {
            color = Color.green;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Yellow)
        {
            color = Color.yellow;
        }
    }
}
