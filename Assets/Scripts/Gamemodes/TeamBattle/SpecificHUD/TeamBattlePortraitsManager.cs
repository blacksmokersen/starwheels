using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer;
using Steamworks;
using System;

public class TeamBattlePortraitsManager : GlobalEventListener
{
    [SerializeField] private List<GameObject> _portraitsList = new List<GameObject>();

    [SerializeField] private List<int> _bindedPlayersID = new List<int>();


    //BOLT

    /*
    public override void OnEvent(LobbyCountdown evnt)
    {
        if (evnt.Time == 0)
        {
            foreach (GameObject player in SWExtensions.KartExtensions.GetAllKarts())
            {
                var playerInfo = player.GetComponent<PlayerInfo>();
                foreach (GameObject portrait in _portraitsList)
                {
                    var teamBattlePortraits = portrait.GetComponent<TeamBattlePortraits>();
                    if (teamBattlePortraits.PortraitTeam == playerInfo.Team
                        && teamBattlePortraits.IsAlreadyBinded == false
                        && !_bindedPlayersID.Contains(playerInfo.OwnerID))
                    {
                        teamBattlePortraits.PlayerBindedID = playerInfo.OwnerID;
                        teamBattlePortraits.IsAlreadyBinded = true;
                        portrait.SetActive(true);
                        _bindedPlayersID.Add(playerInfo.OwnerID);
                    }
                }
            }
        }
    }
    */

    public override void OnEvent(ShareTeamBattlePortraitInfos evnt)
    {
        if (evnt.RemovePlayer)
        {
            RemovePortrait(evnt.playerID);
        }
        else if (evnt.AddPlayer || !_bindedPlayersID.Contains(evnt.playerID))
        {
            AddPortrait(evnt.playerID);
        }
        else
        {
            foreach (GameObject portrait in _portraitsList)
            {
                var teamBattlePortraits = portrait.GetComponent<TeamBattlePortrait>();
                if (teamBattlePortraits.PlayerBindedID == evnt.playerID)
                {
                    teamBattlePortraits.LifeCount = evnt.LifeCount;
                    teamBattlePortraits.SetLifeDisplay(evnt.LifeCount);

                    if (evnt.IsDead)
                    {
                        teamBattlePortraits.Kill();
                    }
                    else if (evnt.IsInJail)
                    {
                        teamBattlePortraits.Jail(true);
                    }
                    else
                    {
                        teamBattlePortraits.Jail(false);
                    }
                }
            }
        }
    }

    // PUBLIC

    public void RemovePortrait(int playerID)
    {
        foreach (GameObject portrait in _portraitsList)
        {
            var teamBattlePortraits = portrait.GetComponent<TeamBattlePortrait>();
            if (teamBattlePortraits.PlayerBindedID == playerID)
            {
                teamBattlePortraits.PlayerBindedID = 0;
                teamBattlePortraits.IsAlreadyBinded = false;
                portrait.SetActive(false);
                _bindedPlayersID.Remove(playerID);
            }
        }
    }

    public void AddPortrait(int playerID)
    {
        var playerInfo = SWExtensions.KartExtensions.GetKartWithID(playerID).GetComponent<PlayerInfo>();
        foreach (GameObject portrait in _portraitsList)
        {
            var teamBattlePortraits = portrait.GetComponent<TeamBattlePortrait>();
            if (teamBattlePortraits.PortraitTeam == playerInfo.Team
                && teamBattlePortraits.IsAlreadyBinded == false
                && !_bindedPlayersID.Contains(playerInfo.OwnerID))
            {
                var steamID = "" + SteamUser.GetSteamID().m_SteamID;

                if (SteamManager.Initialized)
                {
                    teamBattlePortraits.SteamID = new CSteamID() { m_SteamID = Convert.ToUInt64(steamID) };
                    teamBattlePortraits.UpdateAvatar(teamBattlePortraits.SteamID);
                }

                teamBattlePortraits.PlayerBindedID = playerInfo.OwnerID;
                teamBattlePortraits.Jail(false);
                teamBattlePortraits.IsAlreadyBinded = true;
                portrait.SetActive(true);
                _bindedPlayersID.Add(playerID);
            }
        }
    }
}
