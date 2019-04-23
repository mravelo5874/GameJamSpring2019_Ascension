using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaticVariables : MonoBehaviour
{
    public static StaticVariables i = null;

    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerList = new List<player>();
    }

    // Round number:
    public int round_num = 0;


    // number of players:
    public int playerCount = 2;

    // player controllers / colors:
    [System.Serializable]
    public class player
    {
        public PlayerController.PlayerControllerNum pc;
        public PlayerVisual.PlayerColor color;
        public int total_points;
        public int delta_points;
    }

    [SerializeField]
    public List<player> playerList;


    // character / options screen:
    public int pointCount = 15;
    public int pointSelMin = 5;
    public int pointSelMax = 100;
    public int delta_point = 5;

    // points to award
    public int[] win_points;
    public int not_complete_points;
}
