using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public bool isJoined = false;
    public PlayerController.PlayerControllerNum playerNum;
    public int controller_num = 0;
    public bool isReady = false;
    private bool input_allowed = true;

    public PlayerVisual.PlayerColor color;
    public int color_num = 0;

    public GameObject pressAScreen;
    public GameObject JoinedScreen;
    public GameObject ColorDisplayRotation;
    public GameObject[] colorDisplays;
    public Image BG;

    private InputManager im;
    private NewGameManager ngm;

    private void Awake()
    {
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        ngm = GameObject.Find("NewGameManager").GetComponent<NewGameManager>();
    }

    public void AddPlayer(PlayerController.PlayerControllerNum num, int c_num)
    {
        StaticVariables.i.sfxManager.play_cool_select();

        isJoined = true;
        controller_num = c_num;
        playerNum = num;
        ChooseStartingColor();
        StartCoroutine(ScreenDelay());
    }

    public void RemovePlayer()
    {
        StaticVariables.i.sfxManager.play_cancel();

        isJoined = false;
        if (ngm != null)
        {
            ngm.RemoveControllerFromPlayer(controller_num);
            ngm.colorTaken[color_num] = false;
        }
        controller_num = 0;
    }

    private void Update()
    {
        if (!isJoined)
        {
            pressAScreen.SetActive(true);
            JoinedScreen.SetActive(false);
            ColorDisplayRotation.SetActive(false);
        }
        else if (isJoined && !isReady)
        {
            if (input_allowed)
            {
                pressAScreen.SetActive(false);
                JoinedScreen.SetActive(true);
                ColorDisplayRotation.SetActive(true);

                // remove controller from character select
                if (im.BisPushed(playerNum))
                {
                    RemovePlayer();
                    StartCoroutine(ScreenDelay());
                }
                // change color
                if (im.XisPushed(playerNum))
                {
                    ngm.colorTaken[color_num] = false;
                    ChangeColor();
                }
                // ready up
                if (im.AisPushed(playerNum))
                {
                    ReadyUp();
                    StartCoroutine(ScreenDelay());
                }
            }
        }
        else if (isJoined && isReady)
        {
            JoinedScreen.SetActive(false);

            if (input_allowed)
            {
                if (im.BisPushed(playerNum) && !ngm.lockedIn)
                {
                    ReadyDown();
                    StartCoroutine(ScreenDelay());
                }
            }
        }

    }

    public IEnumerator ScreenDelay()
    {
        input_allowed = false;
        yield return new WaitForSeconds(0.1f);
        input_allowed = true;
    }

    private void ChooseStartingColor()
    {
        DisableAllColorDisplays();

        for (int i=0; i<4; i++)
        {
            if (!ngm.colorTaken[i])
            {
                colorDisplays[i].SetActive(true);
                ngm.colorTaken[i] = true;
                this.color = ngm.colors[i];
                this.color_num = i;
                return;
            }
        }
    }

    private void DisableAllColorDisplays()
    {
        for (int i=0; i<4; i++)
        {
            colorDisplays[i].SetActive(false);
        }
    }

    private void ChangeColor()
    {
        StaticVariables.i.sfxManager.play_select();

        for (int i=0; i<4; i++)
        {
            color_num++;
            if (color_num >= 4)
            {
                color_num = 0;
            }

            if (!ngm.colorTaken[color_num])
            {
                ngm.colorTaken[color_num] = true;
                this.color = ngm.colors[color_num];

                DisableAllColorDisplays();
                colorDisplays[color_num].SetActive(true);

                return;
            }
        }
    }

    private void ReadyUp()
    {
        StaticVariables.i.sfxManager.play_ready_horn();

        isReady = true;
        BG.color = new Color(PlayerVisual.ReturnRGB(color).r, PlayerVisual.ReturnRGB(color).g, PlayerVisual.ReturnRGB(color).b, (float)100 / 255);

        this.color = ngm.colors[color_num];
        ngm.colorTaken[color_num] = true;
    }

    private void ReadyDown()
    {
        StaticVariables.i.sfxManager.play_cancel();

        isReady = false;
        BG.color = new Color(Color.white.r, Color.white.g, Color.white.b, (float)50 / 255);
        ngm.colorTaken[color_num] = false;
    }
}
