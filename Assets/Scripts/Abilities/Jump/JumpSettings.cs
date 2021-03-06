﻿using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities Settings/Jump")]
    public class JumpSettings : AbilitySettings
    {
        [Header("Jump Forces")]
        public float FirstJumpForce;
        public float SecondJumpUpForce;
        public float SecondJumpLateralForces;

        [Header("Cancel Velocity")]
        public float TimeBeforeCancelVelocity;
        public float CancelSmootherDuration;

        [Header("Jump Invicibility")]
        public float DelayBeforeInvicibilityDuration;
        public float InvicibilityDuration;

        [Header("Time between First and Second Jump")]
        public float MaxTimeBetweenFirstAndSecondJump;
    }
}
