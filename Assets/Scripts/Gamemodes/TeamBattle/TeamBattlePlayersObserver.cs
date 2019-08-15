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
                Debug.LogError("Player ID : " + player + " PlayerLifeCount : " + _playersLifeCount[player]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            DecreasePlayerHealth(2);
        }
    }

    //PUBLIC

    public void DecreasePlayerHealth(int playerID)
    {
        _playersLifeCount[playerID] --;
        Debug.LogError("Decreased : " + playerID + " PlayerLifeCount");
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
            if (_playersLifeCount[player] >= 1)
            {
                alivePlayers.Add(player);
            }
        }
        return alivePlayers;
    }
}
