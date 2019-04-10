using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointsManager : MonoBehaviour
{
    public GameObject node_prefab;
    public GameObject node_container;

    private void Awake()
    {
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


        // Instantiate nodes nodes of the players
        for (int i = StaticVariables.i.playerCount - 1; i >= 0; i--)
        {
            bool crown = false;
            if ( i== StaticVariables.i.playerCount - 1)
            {
                crown = true;
            }
            GameObject node = Instantiate(node_prefab, node_container.transform);
            node.GetComponent<PlayerPointNode>().DecorateNode(PlayerVisual.ReturnRGB(players[i].color), players[i].total_points, players[i].pc.ToString(), crown);
        }
    }
}
