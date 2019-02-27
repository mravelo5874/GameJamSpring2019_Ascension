using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    public InputManager im;
    public TextMeshProUGUI PlayerCountText;
    public TextMeshProUGUI PointCountText;
    
    public GameObject[] characterSelectBoxes;
    public CharacterSelectManager[] characterSelectManagers;

    public PlayerController.PlayerControllerNum[] playerControllers;
    public bool[] controllerNumTaken;

    private void Awake()
    {
        ResetScene();
        UpdateTexts();
        UpdateCharacterSelect();
    }

    public void ResetScene()
    {
        for (int i = 0; i < 4; i++)
        {
            characterSelectManagers[i].RemovePlayer();
        }
    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (im.JumpIsPushed(playerControllers[i]) && !controllerNumTaken[i])
            {
                for (int j = 0; i < 4; j++)
                {
                    if (!characterSelectManagers[j].isJoined)
                    {
                        characterSelectManagers[j].AddPlayer(playerControllers[i], i);
                        controllerNumTaken[i] = true;
                        return;
                    }
                }
            }
        }
    }

    public void RemoveControllerFromPlayer(int num)
    {
        if (num >= 0 && num <= 3)
        {
            controllerNumTaken[num] = false;
        }
    }

    private void UpdateCharacterSelect()
    {
        for (int i = 0; i < StaticVariables.playerCount; i++)
        {
            characterSelectBoxes[i].SetActive(true);
        }

        for (int i = 4; i > StaticVariables.playerCount; i--)
        {
            characterSelectBoxes[i - 1].SetActive(false);
        }
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
        UpdateCharacterSelect();
    }

    public void IncPlayerCount()
    {
        StaticVariables.playerCount++;
        if(StaticVariables.playerCount > 4)
        {
            StaticVariables.playerCount = 4;
        }

        UpdateTexts();
        UpdateCharacterSelect();
    }

    public void DecPointCount()
    {
        StaticVariables.pointCount -= StaticVariables.delta_point;
        if (StaticVariables.pointCount < StaticVariables.pointSelMin)
        {
            StaticVariables.pointCount = StaticVariables.pointSelMin;
        }

        UpdateTexts();
    }

    public void IncPointCount()
    {
        StaticVariables.pointCount += StaticVariables.delta_point;
        if (StaticVariables.pointCount > StaticVariables.pointSelMax)
        {
            StaticVariables.pointCount = StaticVariables.pointSelMax;
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
