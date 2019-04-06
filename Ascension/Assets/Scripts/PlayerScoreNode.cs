using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreNode : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI playerTime;

    public void SetValues(Color color, string playerNum, float time)
    {
        image.color = color;
        playerText.text = playerNum;
        playerTime.text = time.ToString() + " sec";
    }
}
