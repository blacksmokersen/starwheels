using UnityEngine;

[CreateAssetMenu]
public class EngineSettings : ScriptableObject
{
    public float Speed;
    public float TurnSpeed;
    public float DecelerationFactor = 2f;
}
