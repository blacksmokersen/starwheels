using UnityEngine;

[CreateAssetMenu(menuName = "Kart Settings/Engine")]
public class EngineSettings : ScriptableObject
{
    public float SpeedForce;
    public float MaxMagnitude = 0f;
    public float TurnSpeed;
    public float DecelerationFactor = 2f;
}
