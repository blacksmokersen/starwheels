using UnityEngine;

[CreateAssetMenu(menuName = "Abilities Settings/Jump")]
public class JumpSettings : ScriptableObject
{
    [Header("Jump forces")]
    public float FirstJumpForce;
    public float SecondJumpUpForce;
    public float SecondJumpLateralForces;

    [Header("Cooldown")]
    public float CooldownDuration;
    public float MaxTimeBetweenFirstAndSecondJump;
}
