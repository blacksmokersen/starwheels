using UnityEngine;

[CreateAssetMenu(menuName = "Abilities Settings/Hook")]
public class HookSettings : ScriptableObject
{
    [Header("Hook Prefab")]
    public GameObject Prefab;
    [Header("Effects")]
    public GameObject ReloadParticlePrefab;
    public int ReloadParticleNumber;
    [Header("Cooldown")]
    public float Cooldown;
}
