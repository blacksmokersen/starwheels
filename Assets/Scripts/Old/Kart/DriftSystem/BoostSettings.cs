using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSettings : ScriptableObject {

    [Header("Boost")]
    [Range(0, 1000)] public float BoostSpeed;
    [Range(0, 30)] public float MagnitudeBoost;
    [Range(0, 100)] public float RequiredSpeedToDrift = 12f;
}
