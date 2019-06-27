using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(menuName = "Kart Settings/SteeringWheel")]
    public class SteeringWheelSettings : ScriptableObject
    {
        public float TurnTorque;
        public float MinimumSpeedToSlowOnTurn;
        public float SlowdownTurnValue;
        public float MinimumSpeedToTurn;
        public float MinimumBackSpeedToTurn;
    }
}
