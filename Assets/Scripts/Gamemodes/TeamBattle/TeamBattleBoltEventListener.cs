using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Gamemodes;

public class TeamBattleBoltEventListener : GlobalEventListener
{
    [SerializeField] private IntEvent _onPlayerInfoEvent;
    [SerializeField] private UnityEvent _onAllPlayerInGame;
    [SerializeField] private IntEvent _onPlayerHit;

    private TeamBattleServerRules _teamBattleServerRules;

    //CORE

    private void Awake()
    {
        _teamBattleServerRules = GetComponent<TeamBattleServerRules>();
    }

    public override void OnEvent(PlayerInfoEvent evnt)
    {
        _onPlayerInfoEvent.Invoke(evnt.PlayerID);
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
