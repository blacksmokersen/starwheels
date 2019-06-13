using System.Collections.Generic;
using Multiplayer;
using UnityEngine;
using Bolt;
using UdpKit;
using Bolt.Utils;
using TMPro;

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
        [SerializeField] private LobbyPlayerInfoEntry _nicknameEntryPrefab;

        [Header("UI Text Elements")]
        [SerializeField] private TextMeshProUGUI _currentPlayerCountText;

        private List<LobbyPlayerInfoEntry> _entries = new List<LobbyPlayerInfoEntry>();

        // BOLT

        public override void SessionConnected(UdpSession session, IProtocolToken token)
        {
            var lobbyToken = (LobbyToken)session.GetProtocolToken();
            BuildPlayerList(lobbyToken.PlayersNicknames);
        }

        public override void Connected(BoltConnection connection)
        {
            var joinToken = (JoinToken) connection.ConnectToken;

            if (BoltNetwork.IsServer)
            {
                var playerCount = 1 + SWMatchmaking.GetCurrentLobbyPlayerCount();
                LobbyPlayerJoined lobbyPlayerJoinedEvent = LobbyPlayerJoined.Create();
                lobbyPlayerJoinedEvent.LobbyPlayerCount = playerCount;
                lobbyPlayerJoinedEvent.PlayerID = (int)connection.ConnectionId;
                lobbyPlayerJoinedEvent.PlayerNickname = joinToken.Nickname;
                lobbyPlayerJoinedEvent.Send();
            }
        }

        public override void OnEvent(LobbyPlayerJoined evnt)
        {
            CreatePlayerEntry(evnt.PlayerNickname);
            UpdateCurrentPlayerCount(evnt.LobbyPlayerCount);
            Debug.Log("[LOBBY] A player joined the lobby.");
        }

        public override void Disconnected(BoltConnection connection)
        {
            var joinToken = (JoinToken)connection.ConnectToken;

            if (BoltNetwork.IsServer)
            {
                var playerCount = SWMatchmaking.GetCurrentLobbyPlayerCount();
                LobbyPlayerLeft lobbyPlayerLeftEvent = LobbyPlayerLeft.Create();
                lobbyPlayerLeftEvent.LobbyPlayerCount = playerCount;
                lobbyPlayerLeftEvent.PlayerID = (int)connection.ConnectionId;
                lobbyPlayerLeftEvent.PlayerNickname = joinToken.Nickname;
                lobbyPlayerLeftEvent.Send();
            }
        }

        public override void OnEvent(LobbyPlayerLeft evnt)
        {
            RemovePlayerEntry(evnt.PlayerNickname);
            UpdateCurrentPlayerCount(evnt.LobbyPlayerCount);
            Debug.Log("[LOBBY] A player left the lobby.");
        }

        // PRIVATE

        private void UpdateCurrentPlayerCount(int playerCount)
        {
            _currentPlayerCountText.text = playerCount + " players";

            if (BoltNetwork.IsServer)
            {
                _lobbyData.CurrentPlayers = playerCount;
                SWMatchmaking.SetLobbyData(_lobbyData);
            }
        }

        private void CreatePlayerEntry(string nickname)
        {
            if (!NicknameInList(nickname))
            {
                var entry = Instantiate(_nicknameEntryPrefab, transform, false);
                entry.GetComponent<LobbyPlayerInfoEntry>().SetNickname(nickname);

                _entries.Add(entry);
                _lobbyData.PlayersNicknames.Add(nickname);
            }
            else
            {
                Debug.LogError("[LOBBY] ID already found for the player. Couldn't add another entry.");
            }
        }

        private void BuildPlayerList(List<string> playerList)
        {
            foreach (var playerNickname in playerList)
            {
                CreatePlayerEntry(playerNickname);
            }
        }

        private void RemovePlayerEntry(string nickname)
        {
            if (NicknameInList(nickname))
            {
                _lobbyData.PlayersNicknames.Remove(nickname);
                var entry = FindEntryForNickname(nickname);
                _entries.Remove(entry);
                Destroy(entry);
            }
            else
            {
                Debug.LogError("[LOBBY] No ID found for the player. Couldn't remove his entry.");
            }
        }

        private bool NicknameInList(string nickname)
        {
            foreach (var entry in _entries)
            {
                if (entry.Nickname.Equals(nickname))
                {
                    return true;
                }
            }
            return false;
        }

        private LobbyPlayerInfoEntry FindEntryForNickname(string nickname)
        {
            foreach (var entry in _entries)
            {
                if (entry.Nickname.Equals(nickname))
                {
                    return entry;
                }
            }
            return null;
        }
    }
}
