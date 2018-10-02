using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(menuName = "Kart Settings/SteeringWheel")]
    public class SteeringWheelSettings : ScriptableObject
    {
        public float TurnTorque;
        public float SlowdownTurnValue;
    }
}
