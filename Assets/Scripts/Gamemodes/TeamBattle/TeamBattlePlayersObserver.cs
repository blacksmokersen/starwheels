using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWExtensions;
using Multiplayer;
using Gamemodes;
using Bolt;

public class TeamBattlePlayersObserver : GlobalEventListener
{
    private TeamBattleServerRules _teamBattleServerRules;

    private Dictionary<int, int> _playersLifeCount = new Dictionary<int, int>();
    private Dictionary<int, Team> _playersInJail = new Dictionary<int, Team>();

    //CORE

    private void Awake()
    {
        _teamBattleServerRules = GetComponent<TeamBattleServerRules>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            foreach (int player in _playersLifeCount.Keys)
            {
                Debug.LogError("- Player ID : " + player + " - PlayerLifeCount : " + _playersLifeCount[player]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            foreach (int player in _playersInJail.Keys)
            {
                Debug.LogError("- Player ID : " + player + " - Team : " + _playersInJail[player]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            DecreasePlayerHealth(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddObservedPlayer(0);
            CheckPlayerHealth(0);
        }
    }

    //PUBLIC

    public void DecreasePlayerHealth(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            _playersLifeCount[playerID]--;
            Debug.LogError("Decreased : " + playerID + " PlayerLifeCount");
            CheckPlayerHealth(playerID);
        }
    }

    public void AddObservedPlayer(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            if (!_playersLifeCount.ContainsKey(playerID))
            {
                Debug.LogError("Observe Player : " + playerID);
                _playersLifeCount.Add(playerID, _teamBattleServerRules.TeamBattleSettings.LifeCountPerPlayers);
            }
        }
    }

    public void RemoveObservedPlayer(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            if (_playersLifeCount.ContainsKey(playerID))
            {
                Debug.LogError("REMOVED Player : " + playerID);
                _playersLifeCount.Remove(playerID);
            }
        }
    }

    public void CheckAllKarts()
    {
        if (BoltNetwork.IsServer)
        {
            foreach (int player in _playersLifeCount.Keys)
            {
                if (KartExtensions.GetKartWithID(player) == null)
                {
                    _playersLifeCount.Remove(player);
                    Debug.LogError("REMOVED : " + player);
                }
            }
        }
    }

    public List<int> GetAlivePlayers()
    {
        List<int> alivePlayers = new List<int>();

        foreach (int player in _playersLifeCount.Keys)
        {
            if (_playersInJail.ContainsKey(player))
            {
                Debug.LogError(player + " Is in Jail");
            }
            else if (_playersLifeCount[player] >= 0)
            {
                alivePlayers.Add(player);
            }
        }
        return alivePlayers;
    }

    public void FreePlayersFromJail(string jailTeam)
    {
        foreach (int player in _playersInJail.Keys)
        {
            if (_playersInJail[player].ToString() != jailTeam)
            {
                _playersInJail.Remove(player);
                GameObject kart = KartExtensions.GetKartWithID(player);
                kart.GetComponentInChildren<Health.Health>().UnsetInvincibility();
            }
        }
    }

    //PRIVATE

    private void CheckPlayerHealth(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            if (_playersLifeCount[playerID] == 0)
            {
                if (!_playersInJail.ContainsKey(playerID))
                {
                    Debug.LogError("SEND JAIL EVENT : " + playerID);
                    _playersInJail.Add(playerID, KartExtensions.GetKartWithID(playerID).GetComponent<PlayerInfo>().Team);

                    KartForcedToJail kartForcedtoJailEvent = KartForcedToJail.Create();
                    kartForcedtoJailEvent.PlayerID = playerID;
                    kartForcedtoJailEvent.Team = KartExtensions.GetKartWithID(playerID).GetComponent<PlayerInfo>().Team.ToString();
                    kartForcedtoJailEvent.Send();
                }
            }
            else if (_playersLifeCount[playerID] == -1)
            {
                RemoveObservedPlayer(playerID);

                GameObject playerKart = KartExtensions.GetKartWithID(playerID);

                PermanentDeath permanentdeath = PermanentDeath.Create();
                permanentdeath.PlayerEntity = playerKart.GetComponent<BoltEntity>();
                permanentdeath.TimeBeforeDeath = 0;
                permanentdeath.PlayerID = playerID;
                permanentdeath.PlayerTeam = playerKart.GetComponent<PlayerInfo>().Team.ToString();
                permanentdeath.PlayerTeam = playerKart.GetComponent<PlayerInfo>().Nickname;
                permanentdeath.Send();
            }
        }
    }
}
