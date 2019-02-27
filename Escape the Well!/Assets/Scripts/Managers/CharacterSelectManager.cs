using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public bool isJoined = false;
    public PlayerController.PlayerControllerNum playerNum;
    public int controller_num = 0;
    public PlayerVisual.PlayerColor color;
    public bool isReady = false;

    public GameObject pressAScreen;
    public GameObject JoinedScreen;

    private InputManager im;
    private NewGameManager ngm;

    private void Awake()
    {
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        ngm = GameObject.Find("NewGameManager").GetComponent<NewGameManager>();
    }

    public void AddPlayer(PlayerController.PlayerControllerNum num, int c_num)
    {
        isJoined = true;
        controller_num = c_num;
        playerNum = num;
    }

    public void RemovePlayer()
    {
        isJoined = false;
        if (ngm != null)
        {
            ngm.RemoveControllerFromPlayer(controller_num);
        }
        controller_num = 0;
    }

    private void Update()
    {
        if (!isJoined)
        {
            pressAScreen.SetActive(true);
            JoinedScreen.SetActive(false);
        }
        else
        {
            pressAScreen.SetActive(false);
            JoinedScreen.SetActive(true);

            // remove controller from character select
            if (im.BisPushed(playerNum))
            {
                RemovePlayer();
            }
            // change color, ready up, and remove player
        }
    }
}
