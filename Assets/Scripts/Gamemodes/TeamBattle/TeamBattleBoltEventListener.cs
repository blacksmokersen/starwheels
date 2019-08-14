using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Gamemodes;

public class TeamBattleBoltEventListener : GlobalEventListener
{
    private TeamBattleServerRules _teamBattleServerRules;

    //CORE

    private void Awake()
    {
        _teamBattleServerRules = GetComponent<TeamBattleServerRules>();
    }

}
