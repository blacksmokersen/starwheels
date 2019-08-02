using UnityEngine;
using Bolt;

public class CountDownSoundsScript : GlobalEventListener
{
    [SerializeField] private AudioSource _mapIntroSound;
    [SerializeField] private AudioSource _countdown;

    public override void OnEvent(LobbyCountdown evnt)
    {
        if (evnt.Time == 7)
            _mapIntroSound.Play();

        if (evnt.Time == 3)
            _countdown.Play();
    }
}
