using UnityEngine;

[CreateAssetMenu(menuName = "Camera Settings/CamBoost")]
public class CamBoostSettings : ScriptableObject
{
    [SerializeField] public float BoostDuration;
    [SerializeField] public float StartingDistanceCamInBoost;
    [SerializeField] public float MaxDistanceCamInBoost;
    [SerializeField] public float CamBoostReturnOnKartDelay;
}
