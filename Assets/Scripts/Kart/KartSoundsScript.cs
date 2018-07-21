using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kart
{
    public class KartSoundsScript : MonoBehaviour
    {

        public AudioClip MotorAccel;
        public AudioClip Motor;
        public AudioClip MotorDecel;
        public AudioClip DriftStart;
        public AudioClip Drift;
        public AudioClip DriftEnd;
        public AudioClip FirstJump;
        public AudioClip SecondJump;
        public AudioClip PlayerHit;
        public AudioClip Boost;

        public AudioSource soundManager;

        private void Awake()
        {
          soundManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        }

        public void PlayMotorAccel()
        {
            if (!soundManager.isPlaying)
            {
                soundManager.PlayOneShot(MotorAccel);
            }
        }
        public void PlayMotor()
        {
            if (!soundManager.isPlaying)
            {
                soundManager.clip = Motor;
                soundManager.Play();
            }
        }
        public void PlayMotorDecel()
        {
            if (!soundManager.isPlaying)
            {
                soundManager.PlayOneShot(MotorDecel);
            }
        }
        public void PlayDriftStart()
        {
            soundManager.PlayOneShot(DriftStart);
        }
        public void PlayDrift()
        {
                soundManager.clip = Drift;
                soundManager.Play();
        }
        public void PlayDriftEnd()
        {
            soundManager.PlayOneShot(DriftEnd);
        }
        public void PlayFirstJump()
        {
            soundManager.PlayOneShot(FirstJump);
        }
        public void PlaySecondJump()
        {
            soundManager.PlayOneShot(SecondJump);
        }
        public void Playerhit()
        {
            soundManager.PlayOneShot(PlayerHit);
        }
        public void BoostSound()
        {
            soundManager.PlayOneShot(Boost);
        }
    }
}
