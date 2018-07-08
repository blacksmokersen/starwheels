using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kart;


public class KartEffects : MonoBehaviour
{
    private KartStates kartStates;
    public ParticleSystem smokeLeftWheel;
    public ParticleSystem smokeRightWheel;

    private void Awake()
    {
        kartStates = FindObjectOfType<KartStates>();

        //   smokeLeftWheel = GameObject.Find("smoke1").GetComponent<ParticleSystem>();
        //   smokeRightWheel = GameObject.Find("smoke2").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        DriftSmokeControl();
    }

    void DriftSmokeControl()
    {
        if (kartStates.DriftBoostState == DriftBoostStates.NotDrifting)
        {
            smokeLeftWheel.Stop(true);
            smokeRightWheel.Stop(true);
        }
        else if (kartStates.AirState == AirStates.Grounded)
        {
            var main = smokeLeftWheel.main;
            var main2 = smokeRightWheel.main;

            if (!smokeLeftWheel.isPlaying)
                smokeLeftWheel.Play(true);
            if (!smokeRightWheel.isPlaying)
                smokeRightWheel.Play(true);

            if (kartStates.DriftBoostState == DriftBoostStates.OrangeDrift)
            {
                main.startColor = new Color(255, 255, 0, 255);
                main2.startColor = new Color(255, 255, 0, 255);
            }
            else if (kartStates.DriftBoostState == DriftBoostStates.RedDrift)
            {
                main.startColor = new Color(255, 0, 0, 255);
                main2.startColor = new Color(255, 0, 0, 255);
            }
            else
            {
                main.startColor = new Color(255, 255, 255, 255);
                main2.startColor = new Color(255, 255, 255, 255);
            }
        }
    }
}

