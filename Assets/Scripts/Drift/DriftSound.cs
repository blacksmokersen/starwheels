using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftSound : MonoBehaviour {


    [Header("Drift")]
    public AudioSource StartDrift;
    public AudioSource Drift;
    public AudioSource BoostDrift;
    public AudioSource EndDrift;
    public AudioSource BlueDrift;
    public AudioSource RedDrift;

    public void PlayStartDrift()
    {
        StartDrift.Play();
    }

    public void PlayDrift()
    {
        Drift.Play();
    }

    public void PlayBoostDrift()
    {
        BoostDrift.Play();
    }

    public void PlayEndDrift()
    {
        EndDrift.Play();
    }

    public void StopDrift()
    {
        Drift.Stop();
    }

    public void PlayBlueDrift()
    {
        BlueDrift.Play();
    }

    public void PlayRedDrift()
    {
        RedDrift.Play();
    }
}
