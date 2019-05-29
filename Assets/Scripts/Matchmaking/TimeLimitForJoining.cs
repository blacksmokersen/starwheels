using System.Collections;
using UnityEngine;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class TimeLimitForJoining : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private float _secondsBeforeLobbyGoesPrivate;

        // CORE

        private void Start()
        {
            if (BoltNetwork.IsServer)
            {
                _lobbyData.CanBeJoined = true;
                SWMatchmaking.SetLobbyData(_lobbyData);

                StartCoroutine(SetLobbyPrivateAfterXSeconds(_secondsBeforeLobbyGoesPrivate));
            }
        }

        // PRIVATE

        private IEnumerator SetLobbyPrivateAfterXSeconds(float x)
        {
            yield return new WaitForSeconds(x);

            _lobbyData.CanBeJoined = false;
            SWMatchmaking.SetLobbyData(_lobbyData);
        }
    }
}
