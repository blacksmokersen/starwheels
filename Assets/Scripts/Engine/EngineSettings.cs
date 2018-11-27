using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "Kart Settings/Engine")]
    public class EngineSettings : ScriptableObject
    {
        // public float SpeedForce;
        public AnimationCurve AccelerationCurveVelocity;
        public AnimationCurve DeccelerationCurveVelocity;
        public float SpeedInertiaLoss;
        public float BackSpeedForce;
        public float TurnSpeed;
        public float DecelerationFactor = 2f;
    }
}
