using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameTimer = 0f;
    //public Animator startCountdown;
    public TextMeshProUGUI timerText;
    public GameObject scoresLayoutGroup;
    public GameObject nodePrefab;

    private void Awake()
    {
        //startCountdown.Play("_");
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
        timerText.text = (Mathf.Round(gameTimer * 100f) / 100f).ToString();
    }

    public void PlayerComplete(Color color, string playerNum)
    {
        GameObject node = Instantiate(nodePrefab, scoresLayoutGroup.transform);
        node.GetComponent<PlayerScoreNode>().SetValues(color, playerNum, Mathf.Round(gameTimer * 100f) / 100f);
    }


}
