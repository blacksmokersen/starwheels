using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "Kart Settings/Engine")]
    public class EngineSettings : ScriptableObject
    {
        public float SpeedForce;
        public float TurnSpeed;
        public float DecelerationFactor = 2f;
    }
}
