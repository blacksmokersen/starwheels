using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class CountDownSoundsScript : GlobalEventListener
{

    [SerializeField] private AudioSource _numberCooldown;
    [SerializeField] private AudioSource _areYouReady;

    private bool _alreadyLaunched = false;


    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        if (evnt.IsGameAlreadyStarted)
        {
            _numberCooldown.Play();
            _alreadyLaunched = true;
        }
    }

    public override void OnEvent(LobbyCountdown evnt)
    {
        if (!_alreadyLaunched)
        {
            _numberCooldown.Play();
            _alreadyLaunched = true;
        }
    }
}
