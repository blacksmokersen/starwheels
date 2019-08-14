using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Gamemodes;

public class TeamBattleBoltEventListener : GlobalEventListener
{
    [SerializeField] private UnityEvent _onAllPlayerInGame;
    [SerializeField] private UnityEvent<int> _onPlayerHit;

    private TeamBattleServerRules _teamBattleServerRules;

    //CORE

    private void Awake()
    {
        _teamBattleServerRules = GetComponent<TeamBattleServerRules>();
    }

    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        _onAllPlayerInGame.Invoke();
    }

    public override void OnEvent(PlayerHit evnt)
    {
        _onPlayerHit.Invoke(evnt.VictimID);
    }
}
