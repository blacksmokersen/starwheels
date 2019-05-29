using UnityEngine;
using UnityEngine.UI;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class PrivateLobbySetter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("UI Elements")]
        [SerializeField] private Toggle _toggle;

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(SetPrivateLobby);
            _lobbyData.Public = !_toggle.isOn;
        }

        private void SetPrivateLobby(bool value)
        {
            _lobbyData.Public = !value;
        }
    }
}
