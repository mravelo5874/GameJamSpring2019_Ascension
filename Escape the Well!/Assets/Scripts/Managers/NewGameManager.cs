using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    
    public TextMeshProUGUI PlayerCountText;
    public TextMeshProUGUI PointCountText;

    private void Awake()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        PlayerCountText.text = StaticVariables.playerCount.ToString();
        PointCountText.text = StaticVariables.pointCount.ToString();
    }

    public void DecPlayerCount()
    {
        StaticVariables.playerCount--;
        if (StaticVariables.playerCount < 2)
        {
            StaticVariables.playerCount = 2;
        }

        UpdateTexts();
    }

    public void IncPlayerCount()
    {
        StaticVariables.playerCount++;
        if(StaticVariables.playerCount > 4)
        {
            StaticVariables.playerCount = 4;
        }

        UpdateTexts();
    }

    public void DecPointCount()
    {
        StaticVariables.pointCount -= 20;
        if (StaticVariables.pointCount < 20)
        {
            StaticVariables.pointCount = 20;
        }

        UpdateTexts();
    }

    public void IncPointCount()
    {
        StaticVariables.pointCount += 20;
        if (StaticVariables.pointCount > 500)
        {
            StaticVariables.pointCount = 500;
        }

        UpdateTexts();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToCharacterSelectScene()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }
}
