using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer.Teams;

public class TotemMapSounds : GlobalEventListener
{

    [SerializeField] private AudioSource _blueStep1;
    [SerializeField] private AudioSource _blueStep2;
    [SerializeField] private AudioSource _blueStep3;
    [SerializeField] private AudioSource _blueStep4Win;
    [SerializeField] private AudioSource _redStep1;
    [SerializeField] private AudioSource _redStep2;
    [SerializeField] private AudioSource _redStep3;
    [SerializeField] private AudioSource _redStep4Win;

    private int _wallHitIterationBlue = 0;
    private int _wallHitIterationRed = 0;

    public override void OnEvent(TotemWallHit totemWallHit)
    {
        if (totemWallHit.Team == TeamsColors.RedColor)
        {
            _wallHitIterationRed++;
            switch (_wallHitIterationRed)
            {
                case 1:
                    _redStep1.Play();
                    break;
                case 2:
                    _redStep2.Play();
                    break;
                case 3:
                    _redStep4Win.Play();
                    break;
            }
        }
        else
        {
            _wallHitIterationBlue++;
            switch (_wallHitIterationBlue)
            {
                case 1:
                    _blueStep1.Play();
                    break;
                case 2:
                    _blueStep2.Play();
                    break;
                case 3:
                    _blueStep4Win.Play();
                    break;
            }
        }
    }
}
