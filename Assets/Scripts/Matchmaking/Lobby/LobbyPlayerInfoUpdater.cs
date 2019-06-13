using System.Collections.Generic;
using Multiplayer;
using UnityEngine;
using Bolt;
using UdpKit;
using Bolt.Utils;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class LobbyNicknamesUpdater : GlobalEventListener
    {
        [Header("Player Information")]
        [SerializeField] private PlayerSettings _myPlayerSettings;

        [Header("Lobby Information")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("Prefab")]
        [SerializeField] private GameObject _nicknameEntryPrefab;

        private Dictionary<int, GameObject> _entries = new Dictionary<int, GameObject>();

        // BOLT

        public override void Connected(BoltConnection connection)
        {
            var joinToken = (JoinToken) connection.ConnectToken;
            var entry = Instantiate(_nicknameEntryPrefab, transform, false);
            _entries.Add((int)connection.ConnectionId, entry);

            var playerNickname = ((JoinToken)connection.ConnectToken).Nickname;
            _lobbyData.PlayersNicknames.Add(playerNickname);
        }

        public override void SessionConnected(UdpSession session, IProtocolToken token)
        {
            Debug.LogError("Number of users in session : " + session.ConnectionsCurrent);
            Debug.LogError("Token : " + session.GetProtocolToken());
        }

        public override void Disconnected(BoltConnection connection)
        {
            _entries.Remove((int)connection.ConnectionId);
        }
    }
}
