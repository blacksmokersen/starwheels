using UnityEngine;

[CreateAssetMenu]
public class JumpingAbilitySettings : ScriptableObject
{
    [Header("Jump forces")]
    public float FirstJumpForce;
    public float SecondJumpUpForce;
    public float SecondJumpLateralForces;

    [Header("Cooldown")]
    public float CooldownDuration;
}
