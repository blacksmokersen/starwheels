using UnityEngine;

[CreateAssetMenu(menuName = "Kart Settings/Engine")]
public class EngineSettings : ScriptableObject
{
    public float Speed;
    public float TurnSpeed;
    public float DecelerationFactor = 2f;
}
