using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PointsTableManager : MonoBehaviour
{
    public GameObject node_prefab;
    public GameObject VBox_object;
    public TextMeshProUGUI rounds_text;

    public float switch_scene_delay;
    public bool playerWin;

    private void Awake()
    { 
        // show round number
        rounds_text.text = "ROUND " + StaticVariables.i.round_num;

        // set up bars
        for (int i=0; i<StaticVariables.i.playerCount; i++)
        {
            GameObject node = Instantiate(node_prefab, VBox_object.transform);
            BarManager bm = node.GetComponentInChildren<BarManager>();
            SetColor(bm, i);

            bm.SetName(StaticVariables.i.playerList[i].pc.ToString());
            bm.SetPointRangeToLerp(StaticVariables.i.playerList[i].total_points, StaticVariables.i.playerList[i].delta_points);

            // if player is gaining points
            if (StaticVariables.i.playerList[i].delta_points > 0)
            {
                bm.SetPointBarScale((float)StaticVariables.i.playerList[i].total_points / StaticVariables.i.pointCount);
                bm.SetDeltaBarScale((float)(StaticVariables.i.playerList[i].total_points + StaticVariables.i.playerList[i].delta_points) / StaticVariables.i.pointCount);
                bm.LerpPointBarOverTime(1f, 1f);
            }
            else // player is loosing points
            {
                bm.SetDeltaBarScale((float)StaticVariables.i.playerList[i].total_points / StaticVariables.i.pointCount);
                bm.SetPointBarScale((float)(StaticVariables.i.playerList[i].total_points + StaticVariables.i.playerList[i].delta_points) / StaticVariables.i.pointCount);
                bm.LerpDeltaBarOverTime(1f, 1f);
            }

            // change point values and reset delta points
            StaticVariables.i.playerList[i].total_points += StaticVariables.i.playerList[i].delta_points;
            if (StaticVariables.i.playerList[i].total_points < 0)
            {
                StaticVariables.i.playerList[i].total_points = 0;
            }
            StaticVariables.i.playerList[i].delta_points = 0;
        }


        // check to see if any player won
        for (int i = 0; i < StaticVariables.i.playerCount; i++)
        {
            if (StaticVariables.i.playerList[i].total_points >= StaticVariables.i.pointCount)
            {
                playerWin = true;
            }
        }
        if (playerWin)
        {
            StartCoroutine(EndGameScene());
        }
        else
        {
            StartCoroutine(SwitchScenes());
        }
    }

    private IEnumerator EndGameScene()
    {
        yield return new WaitForSeconds(switch_scene_delay);
        SceneManager.LoadScene("GameResultsScene");
    }

    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(switch_scene_delay);
        SceneManager.LoadScene("RandomSceneLoader");
    }

    private void SetColor(BarManager bm, int num)
    {
        PlayerVisual.PlayerColor player_color = StaticVariables.i.playerList[num].color;
        if (player_color == PlayerVisual.PlayerColor.Blue)
        {
            bm.SetPointBarColor(Color.blue);
        }
        else if (player_color == PlayerVisual.PlayerColor.Red)
        {
            bm.SetPointBarColor(Color.red);
        }
        else if (player_color == PlayerVisual.PlayerColor.Green)
        {
            bm.SetPointBarColor(Color.green);
        }
        else if (player_color == PlayerVisual.PlayerColor.Yellow)
        {
            bm.SetPointBarColor(Color.yellow);
        }
    }
}
