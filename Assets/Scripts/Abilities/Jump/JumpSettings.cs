using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities Settings/Jump")]
    public class JumpSettings : AbilitySettings
    {
        [Header("Jump Forces")]
        public float FirstJumpForce;
        public float SecondJumpUpForce;
        public float SecondJumpLateralForces;

        [Header("Jump Cooldown")]
        public float MaxTimeBetweenFirstAndSecondJump;
    }
}
