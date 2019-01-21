using UnityEngine;

namespace Common.PhysicsUtils
{
    [CreateAssetMenu(menuName = "Tools Settings/ClampSpeed")]
    public class ClampSpeedSettings : ScriptableObject
    {
        [Header("ClampSpeed")]
        public float BaseClampSpeed;
        [Header("Dont Change That Value")]
        public float CurrentClampSpeed;
    }
}
