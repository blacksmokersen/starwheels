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
    [SerializeField] private IntEvent _onPlayerForcedToJail;
    [SerializeField] private StringEvent _onJailButtonPushed;
    [SerializeField] private IntEvent _onPlayerPermaDeath;

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

    public override void OnEvent(KartForcedToJail evnt)
    {
        _onPlayerForcedToJail.Invoke(evnt.PlayerID);
    }

    public override void OnEvent(JailButtonPushed evnt)
    {
        _onJailButtonPushed.Invoke(evnt.Team);
    }

    public override void OnEvent(PermanentDeath evnt)
    {
        _onPlayerPermaDeath.Invoke(evnt.PlayerID);
    }
}
