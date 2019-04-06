using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool debug_mode;
    public float gameStartTime;
    private float gameTimer;

    private int players_complete = 0;

    //public Animator startCountdown;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI RoundsText;
    public GameObject scoresLayoutGroup;
    public GameObject nodePrefab;

    public Transform[] spawnPoints;
    public Transform playerParent;
    public List<PlayerController> playerControllerList;

    public GameObject bluePlayer;
    public GameObject redPlayer;
    public GameObject greenPlayer;
    public GameObject yellowPlayer;

    public Cinemachine.CinemachineTargetGroup targetGroup;
    public Animator CountDownAnim;
    public bool gameStart = false;


    private void Awake()
    {
        CountDownAnim.Play("level_countdown");
        AnimationClip clip = CountDownAnim.runtimeAnimatorController.animationClips[0];
        StartCoroutine(StartGameDelay(clip.length));

        // display round number:
        StaticVariables.i.round_num = StaticVariables.i.round_num + 1;
        RoundsText.text = "ROUND " + StaticVariables.i.round_num;

        gameTimer = gameStartTime;
        targetGroup = GameObject.Find("TARGET_GROUP").GetComponent<CinemachineTargetGroup>();
        playerControllerList = new List<PlayerController>();

        if (debug_mode)
        {
            gameTimer = 999f;
            StaticVariables.i.playerCount = 2;
            var player = new StaticVariables.player();
            player.pc = PlayerController.PlayerControllerNum.P1;
            player.color = PlayerVisual.PlayerColor.Blue;
            StaticVariables.i.playerList.Add(player);

            var dummy = new StaticVariables.player();
            dummy.pc = PlayerController.PlayerControllerNum.P2;
            dummy.color = PlayerVisual.PlayerColor.Red;
            StaticVariables.i.playerList.Add(dummy);
        }

        // activate players
        for (int i = 0; i < StaticVariables.i.playerCount; i++)
        {
            GameObject player = new GameObject();

            // get the correct colored prefab player
            if (StaticVariables.i.playerList[i].color == PlayerVisual.PlayerColor.Blue)
            {
                player = bluePlayer;
            }
            else if (StaticVariables.i.playerList[i].color == PlayerVisual.PlayerColor.Red)
            {
                player = redPlayer;
            }
            else if (StaticVariables.i.playerList[i].color == PlayerVisual.PlayerColor.Green)
            {
                player = greenPlayer;
            }
            else if (StaticVariables.i.playerList[i].color == PlayerVisual.PlayerColor.Yellow)
            {
                player = yellowPlayer;
            }

            // instantiate player and add to playerControllerList
            player = Instantiate(player, spawnPoints[i].position, Quaternion.identity, playerParent);
            playerControllerList.Add(player.GetComponent<PlayerController>());

            // add to target group
            targetGroup.AddMember(player.transform, 1f, 0f);

            // add correct controller to player
            player.GetComponent<PlayerController>().PlayerNum = StaticVariables.i.playerList[i].pc;
            player.GetComponent<PlayerController>().svp = StaticVariables.i.playerList[i];

            // make player unable to move
            player.GetComponent<PlayerController>().immoblie = true;

            // Odd numbered players start facing left
            if (i % 2 != 0)
            {
                player.GetComponent<PlayerController>().startFacingLeft = true;
            }
        }
    }

    private IEnumerator StartGameDelay(float _time)
    {
        yield return new WaitForSeconds(_time);

        // start update loop
        gameStart = true;

        // make players mobile
        for (int i=0; i<StaticVariables.i.playerCount; i++)
        {
            playerControllerList[i].immoblie = false;
        }
    }

    private void Update()
    {
        if (gameStart)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer < 0f)
            {
                gameTimer = 0f;
            }
            timerText.text = (Mathf.Round(gameTimer * 100f) / 100f).ToString();


            // check to see if all players have completed the round
            var win_count = 0;
            for (int i = 0; i < playerControllerList.Count; i++)
            {
                if (playerControllerList[i].isWin)
                {
                    win_count++;
                }
            }
            if (win_count == playerControllerList.Count)
            {
                EndRound();
            }

            // check to see if game timer is up
            if (gameTimer <= 0f)
            {
                EndRound();
            }
        } 
    }

    private void EndRound()
    {
        // check to see if any players have not completed the round
        for (int i=0; i<StaticVariables.i.playerCount; i++)
        {
            PlayerController pc = playerControllerList[i];
            if (!pc.isWin)
            {
                pc.Deactivate();
                pc.svp.delta_points = StaticVariables.i.not_complete_points;
            }

        }

        // change to point screen scene
        SceneManager.LoadScene("PointsTableScene");
    }

    public void PlayerComplete(Color color, string playerNum, PlayerController _pc)
    {
        GameObject node = Instantiate(nodePrefab, scoresLayoutGroup.transform);
        node.GetComponent<PlayerScoreNode>().SetValues(color, playerNum, gameStartTime - Mathf.Round(gameTimer * 100f) / 100f);

        // assign delta points
        _pc.svp.delta_points = StaticVariables.i.win_points[players_complete];
        players_complete++;
    }
}
