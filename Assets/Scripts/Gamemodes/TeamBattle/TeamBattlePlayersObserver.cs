using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWExtensions;
using Multiplayer;
using Gamemodes;

public class TeamBattlePlayersObserver : MonoBehaviour
{
    private TeamBattleServerRules _teamBattleServerRules;

    private Dictionary<int, int> _playersLifeCount = new Dictionary<int, int>();

    //CORE

    private void Awake()
    {
        _teamBattleServerRules = GetComponent<TeamBattleServerRules>();
    }

    //PUBLIC

    public void SetAllKarts()
    {
        foreach (GameObject player in KartExtensions.GetAllKarts())
        {
            _playersLifeCount.Add(player.GetComponent<PlayerInfo>().OwnerID, 5);
        }
    }

    public void CheckAllKarts()
    {
        foreach (int player in _playersLifeCount.Keys)
        {
            if (KartExtensions.GetKartWithID(player) == null)
            {
                _playersLifeCount.Remove(player);
            }
        }
    }

    public void AddPlayerLifeCount(int playerID, int lifeCount)
    {
        _playersLifeCount.Add(playerID, lifeCount);
    }

    public void RemovePlayerLifeCount(int playerID, int lifeCount)
    {
        if (_playersLifeCount.ContainsKey(playerID))
        {
            _playersLifeCount.Remove(playerID);
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
