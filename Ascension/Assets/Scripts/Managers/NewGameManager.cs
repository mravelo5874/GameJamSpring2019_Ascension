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

    public PlayerVisual.PlayerColor[] colors;
    public bool[] colorTaken;

    public PlayerController.PlayerControllerNum[] playerControllers;
    public bool[] controllerNumTaken;

    public Animator Fader;
    public bool lockedIn = false;

    private void Awake()
    {
        Fader.Play("FadeFromBlack");
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
            if (im.AisPushed(playerControllers[i]) && !controllerNumTaken[i])
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

        // check if all players are ready
        var ready_count = 0;
        for (int i=0; i<4; i++)
        {
            if (characterSelectManagers[i].isReady)
            {
                ready_count++;
            }
        }
        if (ready_count == StaticVariables.i.playerCount)
        {
            lockedIn = true;
            Fader.Play("FadeToBlack");
            StartCoroutine(LoadGame());
        }
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2f);
        StartGame();
    }

    private void StartGame()
    {
        // start on round zero:
        StaticVariables.i.round_num = 0;

        // clear all players
        if (StaticVariables.i.playerList.Count > 0)
        {
            StaticVariables.i.playerList.Clear();
        }

        // add players
        for (int i=0; i<StaticVariables.i.playerCount; i++)
        {
            var player = new StaticVariables.player();
            player.pc = characterSelectManagers[i].playerNum;
            player.color = characterSelectManagers[i].color;
            player.total_points = 0;
            player.delta_points = 0;
            StaticVariables.i.playerList.Add(player);
        }

        // load game:
        SceneManager.LoadScene("LoadingGameScene");
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
        for (int i = 0; i < StaticVariables.i.playerCount; i++)
        {
            characterSelectBoxes[i].SetActive(true);
        }

        for (int i = 4; i > StaticVariables.i.playerCount; i--)
        {
            characterSelectBoxes[i - 1].SetActive(false);
        }
    }

    private void UpdateTexts()
    {
        PlayerCountText.text = StaticVariables.i.playerCount.ToString();
        PointCountText.text = StaticVariables.i.pointCount.ToString();
    }

    public void DecPlayerCount()
    {
        StaticVariables.i.playerCount--;
        if (StaticVariables.i.playerCount < 2)
        {
            StaticVariables.i.playerCount = 2;
        }

        UpdateTexts();
        UpdateCharacterSelect();
    }

    public void IncPlayerCount()
    {
        StaticVariables.i.playerCount++;
        if(StaticVariables.i.playerCount > 4)
        {
            StaticVariables.i.playerCount = 4;
        }

        UpdateTexts();
        UpdateCharacterSelect();
    }

    public void DecPointCount()
    {
        StaticVariables.i.pointCount -= StaticVariables.i.delta_point;
        if (StaticVariables.i.pointCount < StaticVariables.i.pointSelMin)
        {
            StaticVariables.i.pointCount = StaticVariables.i.pointSelMin;
        }

        UpdateTexts();
    }

    public void IncPointCount()
    {
        StaticVariables.i.pointCount += StaticVariables.i.delta_point;
        if (StaticVariables.i.pointCount > StaticVariables.i.pointSelMax)
        {
            StaticVariables.i.pointCount = StaticVariables.i.pointSelMax;
        }

        UpdateTexts();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
