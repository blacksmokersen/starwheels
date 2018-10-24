using UnityEngine;

[CreateAssetMenu(menuName = "Abilities Settings/TPBack")]
public class TPBackSettings : ScriptableObject
{
    [Header("TPBack Prefab")]
    public GameObject Prefab;
    [Header("Effects")]
    public GameObject ReloadParticlePrefab;
    public int ReloadParticleNumber;
    [Header("Cooldown")]
    public float Cooldown;
}
