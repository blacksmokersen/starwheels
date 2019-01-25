using UnityEngine;

namespace Photon
{
    [CreateAssetMenu(menuName = "Lobby Settings/Global Settings")]
    public class LobbySettings : ScriptableObject
    {
        [Header("Countdown Before Game")]
        public bool Countdown;
        public float CountdownSeconds;
    }
}
