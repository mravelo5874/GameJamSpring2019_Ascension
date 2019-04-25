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

    public static Color ReturnRGB(PlayerVisual.PlayerColor _color)
    {
        if (_color == PlayerVisual.PlayerColor.Blue)
        {
            return Color.blue;
        }
        else if (_color == PlayerVisual.PlayerColor.Red)
        {
            return Color.red;
        }
        else if (_color == PlayerVisual.PlayerColor.Green)
        {
            return Color.green;
        }
        else if (_color == PlayerVisual.PlayerColor.Yellow)
        {
            return Color.yellow;
        }
        return Color.gray;
    }
}
