using System.Collections.Generic;
using Multiplayer;
using UnityEngine;
using Bolt;
using UdpKit;
using Bolt.Utils;
using TMPro;
using Multiplayer.Teams;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class LobbyPlayerInfoUpdater : GlobalEventListener
    {
        [Header("Player Information")]
        [SerializeField] private PlayerSettings _myPlayerSettings;

        [Header("Lobby Information")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private TeamsListSettings _completeTeamList;

        [Header("Prefab")]
        [SerializeField] private GameObject _nicknameEntryPrefab;

        [Header("UI Text Elements")]
        [SerializeField] private TextMeshProUGUI _currentPlayerCountText;

        private List<LobbyPlayerInfoEntry> _entries = new List<LobbyPlayerInfoEntry>();
        private List<string> _serverSidePlayerNicknameList = new List<string>();
        private bool _sessionCreated = false;

        private Dictionary<string, string> _redTeamList = new Dictionary<string, string>();
        private Dictionary<string, string> _blueTeamList = new Dictionary<string, string>();

        // BOLT
        public override void SessionCreated(UdpSession session)
        {
            if (!_sessionCreated)
            {
                _serverSidePlayerNicknameList.Add(_myPlayerSettings.Nickname);
                SendNicknameListToAllPlayers();
                _sessionCreated = true;
            }
        }

        public override void Connected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                var joinToken = (JoinToken)connection.ConnectToken;
                _serverSidePlayerNicknameList.Add(joinToken.Nickname);
                SendNicknameListToAllPlayers();
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                var leaveToken = (JoinToken)connection.ConnectToken;
                _serverSidePlayerNicknameList.Remove(leaveToken.Nickname);
                SendNicknameListToAllPlayers();
            }
        }

        public override void OnEvent(LobbyPlayerJoined evnt)
        {
            if (evnt.CleanEntries)
            {
                ResetList();
            }
            else
            {
                CreatePlayerEntry(evnt.PlayerNickname);
                UpdateCurrentPlayerCount(evnt.LobbyPlayerCount);
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            CleanList();
        }

        public override void OnEvent(TeamColorChangeRequest evnt)
        {
            if (BoltNetwork.IsServer)
            {
                if (_lobbyData.ChosenGamemode == Constants.Gamemodes.FFA)
                {
                    CycleThroughTeams(evnt.PlayerNickname, evnt.PlayerActualTeam);
                }
                else if (_lobbyData.ChosenGamemode == Constants.Gamemodes.Battle)
                {

                }
                else if (_lobbyData.ChosenGamemode == Constants.Gamemodes.Totem)
                {

                }
                else
                {
                    Debug.LogError("No chosen gamemode");
                }
            }
        }

        public override void OnEvent(UpdateTeamColorInLobby evnt)
        {
            if (_myPlayerSettings.Nickname == evnt.PlayerNickname)
            {
                Debug.LogError("SET TEAM TO : " + evnt.PlayerTeamColor);
                // _myPlayerSettings.Team == evnt.PlayerTeamColor;
            }
        }

        //PUBLIC

        public void ResetServerList()
        {
            if (BoltNetwork.IsServer)
            {
                _serverSidePlayerNicknameList.Clear();
            }
        }

        //PRIVATE


        private void CycleThroughTeams(string nickname, int team)
        {
            if(team == 11 || team == 0)
            {
                team = 1;
            }
            team += 1;
            if (team == 1)
            {
                team += 1;
            }

            UpdateTeamColorInLobby updateTeamColorInLobby = UpdateTeamColorInLobby.Create();
            updateTeamColorInLobby.PlayerNickname = nickname;
            updateTeamColorInLobby.PlayerTeamColor = team;
            updateTeamColorInLobby.Send();
        }



        private void SendNicknameListToAllPlayers()
        {
            SendResetListEvent();

            var playerCount = 1 + SWMatchmaking.GetCurrentLobbyPlayerCount();
            foreach (string nickname in _serverSidePlayerNicknameList)
            {
                LobbyPlayerJoined lobbyPlayerJoinedEvent = LobbyPlayerJoined.Create();
                lobbyPlayerJoinedEvent.LobbyPlayerCount = playerCount;
                lobbyPlayerJoinedEvent.PlayerNickname = nickname;
                lobbyPlayerJoinedEvent.Send();
            }
        }

        private void SendResetListEvent()
        {
            LobbyPlayerJoined lobbyPlayerJoinedEvent = LobbyPlayerJoined.Create();
            lobbyPlayerJoinedEvent.CleanEntries = true;
            lobbyPlayerJoinedEvent.Send();
        }

        private void CreatePlayerEntry(string nickname)
        {
            if (!NicknameInList(nickname))
            {
                var entry = Instantiate(_nicknameEntryPrefab, transform, false).GetComponent<LobbyPlayerInfoEntry>();
                entry.SetNickname(nickname);

                _entries.Add(entry);
                _lobbyData.PlayersNicknames.Add(nickname);
            }
            else
            {
                Debug.LogError("[LOBBY] ID already found for the player. Couldn't add another entry.");
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

        private void UpdateCurrentPlayerCount(int playerCount)
        {
            _currentPlayerCountText.text = playerCount + " players";

            if (BoltNetwork.IsServer)
            {
                _lobbyData.CurrentPlayers = playerCount;
                SWMatchmaking.UpdateLobbyData(_lobbyData);
            }
        }

        private void ResetList()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            _entries.Clear();
        }

        private void CleanList()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            _entries.Clear();
            _lobbyData.PlayersNicknames.Clear();
            UpdateCurrentPlayerCount(0);
            _sessionCreated = false;
        }

        #region OLD
        /*
    public override void SessionCreated(UdpSession session)
    {
        if (!_sessionCreated)
        {
            CreatePlayerEntry(_myPlayerSettings.Nickname);
            UpdateCurrentPlayerCount(1);

            _sessionCreated = true;
        }
    }

    public override void SessionConnected(UdpSession session, IProtocolToken token)
    {
        var lobbyToken = (LobbyToken)session.GetProtocolToken();
        BuildPlayerList(lobbyToken.PlayersNicknames);
    }

    public override void Connected(BoltConnection connection)
    {
        var joinToken = (JoinToken)connection.ConnectToken;

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
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        CleanList();
    }

    // PRIVATE

    private void UpdateCurrentPlayerCount(int playerCount)
    {
        _currentPlayerCountText.text = playerCount + " players";

        if (BoltNetwork.IsServer)
        {
            _lobbyData.CurrentPlayers = playerCount;
            SWMatchmaking.UpdateLobbyData(_lobbyData);
        }
    }

    private void CreatePlayerEntry(string nickname)
    {
        if (!NicknameInList(nickname))
        {
            var entry = Instantiate(_nicknameEntryPrefab, transform, false).GetComponent<LobbyPlayerInfoEntry>();
            entry.SetNickname(nickname);

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
            Destroy(entry.gameObject);
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

    private void CleanList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        _entries.Clear();
        _lobbyData.PlayersNicknames.Clear();
        _sessionCreated = false;
    }
    */

        #endregion
    }
}
