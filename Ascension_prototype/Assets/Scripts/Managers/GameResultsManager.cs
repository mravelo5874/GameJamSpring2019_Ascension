using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameResultsManager : MonoBehaviour
{
    public GameObject object_1;
    public GameObject object_2;
    public GameObject object_3;
    public GameObject object_4;

    public Animator first_place_anim;
    public Animator second_place_anim;
    public Animator third_place_anim;
    public Animator fourth_place_anim;

    public TextMeshProUGUI name_text_1;
    public TextMeshProUGUI points_text_1;

    public TextMeshProUGUI name_text_2;
    public TextMeshProUGUI points_text_2;

    public TextMeshProUGUI name_text_3;
    public TextMeshProUGUI points_text_3;

    public TextMeshProUGUI name_text_4;
    public TextMeshProUGUI points_text_4;

    public Animator Fader;


    private void Awake()
    {
        Fader.Play("FadeFromBlack");
        StaticVariables.i.sfxManager.play_win_scene();

        // make a copy of the list of players
        List<StaticVariables.player> players = new List<StaticVariables.player>();
        for (int i = 0; i < StaticVariables.i.playerCount; i++)
        {
            StaticVariables.player player = new StaticVariables.player();
            player = StaticVariables.i.playerList[i];
            players.Add(player);
        }

        //Sort the players in order(bubble sort)
        for (int i = 0; i < StaticVariables.i.playerCount - 1; i++)
        {
            for (int j = 0; j < StaticVariables.i.playerCount - i - 1; j++)
            {
                if (players[j].total_points > players[j + 1].total_points)
                {
                    // swap the two players
                    StaticVariables.player temp = players[j];
                    players[j] = players[j + 1];
                    players[j + 1] = temp;
                }
            }
        }

        players.Reverse();

        // place the players in the correct places
        name_text_1.text = "1st: [" + players[0].pc.ToString() + "]";
        points_text_1.text = "PTS: " + players[0].total_points;
        if (players[0].color == PlayerVisual.PlayerColor.Blue)
        {
            first_place_anim.Play("blue_win");
        }
        else if (players[0].color == PlayerVisual.PlayerColor.Red)
        {
            first_place_anim.Play("red_win");
        }
        else if (players[0].color == PlayerVisual.PlayerColor.Green)
        {
            first_place_anim.Play("green_win");
        }
        else if (players[0].color == PlayerVisual.PlayerColor.Yellow)
        {
            first_place_anim.Play("yellow_win");
        }

        name_text_2.text = "2nd: [" + players[1].pc.ToString() + "]";
        points_text_2.text = "PTS: " + players[1].total_points;
        if (players[1].color == PlayerVisual.PlayerColor.Blue)
        {
            second_place_anim.Play("blue_lose");
        }
        else if (players[1].color == PlayerVisual.PlayerColor.Red)
        {
            second_place_anim.Play("red_lose");
        }
        else if (players[1].color == PlayerVisual.PlayerColor.Green)
        {
            second_place_anim.Play("green_lose");
        }
        else if (players[1].color == PlayerVisual.PlayerColor.Yellow)
        {
            second_place_anim.Play("yellow_lose");
        }

        if (players.Count >= 3)
        {
            name_text_3.text = "3rd: [" + players[2].pc.ToString() + "]";
            points_text_3.text = "PTS: " + players[2].total_points;
            if (players[2].color == PlayerVisual.PlayerColor.Blue)
            {
                third_place_anim.Play("blue_lose");
            }
            else if (players[2].color == PlayerVisual.PlayerColor.Red)
            {
                third_place_anim.Play("red_lose");
            }
            else if (players[2].color == PlayerVisual.PlayerColor.Green)
            {
                third_place_anim.Play("green_lose");
            }
            else if (players[2].color == PlayerVisual.PlayerColor.Yellow)
            {
                third_place_anim.Play("yellow_lose");
            }
        }
        else
        {
            object_3.SetActive(false);
        }

        if (players.Count >= 4)
        {
            name_text_4.text = "4th: [" + players[3].pc.ToString() + "]";
            points_text_4.text = "PTS: " + players[3].total_points;
            if (players[3].color == PlayerVisual.PlayerColor.Blue)
            {
                fourth_place_anim.Play("blue_lose");
            }
            else if (players[3].color == PlayerVisual.PlayerColor.Red)
            {
                fourth_place_anim.Play("red_lose");
            }
            else if (players[3].color == PlayerVisual.PlayerColor.Green)
            {
                fourth_place_anim.Play("green_lose");
            }
            else if (players[3].color == PlayerVisual.PlayerColor.Yellow)
            {
                fourth_place_anim.Play("yellow_lose");
            }
        }
        else
        {
            object_4.SetActive(false);
        }


        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(5f);
        Fader.Play("FadeToBlack");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MenuScene");
    }
}
