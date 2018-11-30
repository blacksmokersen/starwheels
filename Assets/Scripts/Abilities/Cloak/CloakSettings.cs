using UnityEngine;

[CreateAssetMenu(menuName = "Abilities Settings/Cloak")]
public class CloakSettings : ScriptableObject
{
    [Header("Duration")]
    public float CloakDuration;

    [Header("Cooldown")]
    public float CooldownDuration;
}
