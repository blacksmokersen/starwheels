using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "Kart Settings/Engine")]
    public class EngineSettings : ScriptableObject
    {
        // public float SpeedForce;
        public AnimationCurve CurveVelocity;
        public float BackSpeedForce;
        public float TurnSpeed;
        public float DecelerationFactor = 2f;
    }
}
