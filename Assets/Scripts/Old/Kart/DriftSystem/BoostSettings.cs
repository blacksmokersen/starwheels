using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools Settings/Boost")]
public class BoostSettings : ScriptableObject {

    [Header("Boost")]
    [Range(0, 10)] public float BoostDuration;
    [Range(0, 1000)] public float BoostSpeed;
    [Range(0, 30)] public float MagnitudeBoost;
    [Range(0, 2)] public float BoostPowerImpulse;

    public float MaxMagnitude;
    public float Speed;
    public float _controlMagnitude;
    public float _controlSpeed;
    public float _currentTimer;

}
