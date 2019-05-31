using UnityEngine;
using TMPro;

namespace SW.Matchmaking.Versioning
{
    public class Versioning : MonoBehaviour
    {
        public string Version;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _versionText;

        [Header("Lobby Data")]
        [SerializeField] private LobbyData _lobbyData;

        private void Awake()
        {
            _versionText.text = "Version " + Version;
            _lobbyData.Version = Version;
        }
    }
}
