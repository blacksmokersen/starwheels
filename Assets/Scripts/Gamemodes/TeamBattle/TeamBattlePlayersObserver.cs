using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBattlePlayersObserver : MonoBehaviour
{
    private Dictionary<int, int> _playersLifeCount = new Dictionary<int, int>();

    //PUBLIC

    public void SetPlayerLifeCount(int playerID, int lifeCount)
    {
        _playersLifeCount.Add(playerID, lifeCount);
    }

    public List<int> GetAlivePlayers()
    {
        List<int> alivePlayers = new List<int>();

        foreach (int player in _playersLifeCount.Keys)
        {
            if(_playersLifeCount[player] >= 1)
            {
                alivePlayers.Add(player);
            }
        }
        return alivePlayers;
    }
}
