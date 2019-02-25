using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public enum PlayerColor { Black, Red, Blue, Green, Yellow, Magenta, Gray, Cyan };

    public PlayerVisual.PlayerColor playerColor;
    public SpriteRenderer body;
    public SpriteRenderer punch;
    public Color color;

    private void Awake()
    {
        SetPlayerColor();
    }

    private void SetPlayerColor()
    {
        if (playerColor == PlayerVisual.PlayerColor.Black)
        {
            body.color = Color.black;
            punch.color = Color.black;
            color = Color.black;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Red)
        {
            body.color = Color.red;
            punch.color = Color.red;
            color = Color.red;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Blue)
        {
            body.color = Color.blue;
            punch.color = Color.blue;
            color = Color.blue;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Green)
        {
            body.color = Color.green;
            punch.color = Color.green;
            color = Color.green;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Yellow)
        {
            body.color = Color.yellow;
            punch.color = Color.yellow;
            color = Color.yellow;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Magenta)
        {
            body.color = Color.magenta;
            punch.color = Color.magenta;
            color = Color.magenta;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Gray)
        {
            body.color = Color.gray;
            punch.color = Color.gray;
            color = Color.gray;
        }
        else if (playerColor == PlayerVisual.PlayerColor.Cyan)
        {
            body.color = Color.cyan;
            punch.color = Color.cyan;
            color = Color.cyan;
        }
    }
}
