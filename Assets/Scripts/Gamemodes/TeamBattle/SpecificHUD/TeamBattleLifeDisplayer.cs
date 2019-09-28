using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer;

public class TeamBattleLifeDisplayer : GlobalEventListener
{
    [SerializeField] private GameObject[] _lifes;

    private int _playerLifesCount = 5;

    public override void OnEvent(PlayerHit evnt)
    {
        var playerID = SWExtensions.KartExtensions.GetMyKart().GetComponent<PlayerInfo>().OwnerID;

        if (playerID == evnt.VictimID)
        {
            DeacreaseLife();
        }
    }

    private void DeacreaseLife()
    {
        if (_playerLifesCount > 0)
        {
            _lifes[_playerLifesCount-1].SetActive(false);
            _playerLifesCount--;
        }
    }
}
