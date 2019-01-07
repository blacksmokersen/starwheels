using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(menuName = "Multiplayer Settings/Spawn Settings")]
    public class SpawnSettings : ScriptableObject
    {
        public bool RespawnOn;
        public float SecondsBeforeRespawn;
    }
}
