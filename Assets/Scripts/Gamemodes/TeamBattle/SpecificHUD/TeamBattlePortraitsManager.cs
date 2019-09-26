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

    private Dictionary<int, string> _playerSteamID = new Dictionary<int, string>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            foreach (int player in _playerSteamID.Keys)
            {
                Debug.LogError("- Player ID : " + player + " - PlayerSteamID : " + _playerSteamID[player]);
            }
        }
    }

    public override void OnEvent(PlayerReady evnt)
    {
        if (!_playerSteamID.ContainsKey(evnt.PlayerID))
        {
            Debug.LogError("STEAM ID PlayerAllStats : " + evnt.SteamID);
            _playerSteamID.Add(evnt.PlayerID, evnt.SteamID);
        }
    }

    public override void OnEvent(PlayerAllStats evnt)
    {
        if (!_playerSteamID.ContainsKey(evnt.PlayerID))
        {
            Debug.LogError("STEAM ID PlayerAllStats : " + evnt.SteamID);
            _playerSteamID.Add(evnt.PlayerID, evnt.SteamID);
        }
    }

    private void Start()
    {
        var serverID = SWExtensions.KartExtensions.GetMyKart().GetComponent<PlayerInfo>().OwnerID;
        if (SteamManager.Initialized)
        {
            var steamID = "" + SteamUser.GetSteamID().m_SteamID;

            if (!_playerSteamID.ContainsKey(serverID))
            {
                Debug.LogError("STEAM ID PlayerAllStats : " + serverID + "  " + steamID);
                _playerSteamID.Add(serverID, steamID);
            }
        }
    }


    /*
    public override void OnEvent(LobbyCountdown evnt)
    {
        if (BoltNetwork.IsServer && evnt.Time == 5)
        {
            var serverID = SWExtensions.KartExtensions.GetMyKart().GetComponent<PlayerInfo>().OwnerID;
            if (!_playerSteamID.ContainsKey(serverID))
            {
                Debug.LogError("STEAM ID PlayerAllStats : " + serverID);
                _playerSteamID.Add(serverID, "" + SteamUser.GetSteamID().m_SteamID);
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
                if (SteamManager.Initialized)
                {
                    if (_playerSteamID.ContainsKey(playerID))
                    {
                        teamBattlePortraits.SteamID = new CSteamID() { m_SteamID = Convert.ToUInt64(_playerSteamID[playerID]) };
                        teamBattlePortraits.UpdateAvatar(teamBattlePortraits.SteamID);
                    }
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
