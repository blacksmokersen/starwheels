using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DriftSettings : ScriptableObject
{
    [Header("Drift")]
    public float DriftGlideOrientation = 500f;
    public float DriftGlideBack = 500f;
    public float DriftTurnSpeed = 150f;
    public float MaxInteriorAngle = 400f;
    public float MaxExteriorAngle = 40f;
    [Range(0, 2)] public float BoostPowerImpulse;

    [Header("Time")]
    [Range(0, 10)] public float TimeBetweenDrifts;
    [Range(0, 10)] public float BoostDuration;

    [Header("Angles")]
    [Range(0, 90)] public float ForwardMaxAngle;
    [Range(0, -90)] public float BackMaxAngle;
}
