﻿using UnityEngine;
using Bolt;

namespace Totem
{
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

        [SerializeField] private AudioSource _crowd1;
        [SerializeField] private AudioSource _crowd2;
        [SerializeField] private AudioSource _crowd3;
        [SerializeField] private AudioSource _crowdStep4Win;

        private int _wallHitIterationBlue = 0;
        private int _wallHitIterationRed = 0;

        public override void OnEvent(TotemWallHit evnt)
        {
            if (evnt.Team.ToTeam() == Team.Red)
            {
                _wallHitIterationRed++;
                switch (_wallHitIterationRed)
                {
                    case 1:
                        _redStep1.Play();
                        PlayRandomCrowdSound();
                        break;
                    case 2:
                        _redStep2.Play();
                        PlayRandomCrowdSound();
                        break;
                    case 3:
                        _redStep4Win.Play();
                        _crowdStep4Win.Play();
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
                        PlayRandomCrowdSound();
                        break;
                    case 2:
                        _blueStep2.Play();
                        PlayRandomCrowdSound();
                        break;
                    case 3:
                        _blueStep4Win.Play();
                        _crowdStep4Win.Play();
                        break;
                }
            }
        }

        private void PlayRandomCrowdSound()
        {
            int randomInt = Random.Range(1, 3);
            switch (randomInt)
            {
                case 1:
                    _crowd1.Play();
                    break;
                case 2:
                    _crowd2.Play();
                    break;
                case 3:
                    _crowd3.Play();
                    break;
            }
        }
    }
}
