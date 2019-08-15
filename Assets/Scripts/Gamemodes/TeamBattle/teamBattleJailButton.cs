using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiplayer;

public class teamBattleJailButton : MonoBehaviour
{
    [SerializeField] private Team _jailButtonTeam;

    private void OnTriggerEnter(Collider player)
    {
        if (player.GetComponent<PlayerInfo>().Team != _jailButtonTeam)
        {
            // lancer bolt event
        }
    }
}
