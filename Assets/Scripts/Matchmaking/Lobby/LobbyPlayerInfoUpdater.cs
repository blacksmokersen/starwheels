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

        private Dictionary<string, int> _redTeamList = new Dictionary<string, int>();
        private Dictionary<string, int> _blueTeamList = new Dictionary<string, int>();


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach (string nick in _lobbyData.PlayersTeamDictionary.Keys)
                {
                    Debug.LogError(nick + " TEAM IS : " + _lobbyData.PlayersTeamDictionary[nick]);
                }
            }
        }

        // BOLT

        public override void SessionCreated(UdpSession session)
        {
            if (!_sessionCreated)
            {
                _serverSidePlayerNicknameList.Add(_myPlayerSettings.Nickname);
                _lobbyData.PlayersTeamDictionary.Add(_myPlayerSettings.Nickname, 0);
                SendNicknameListToAllPlayers();
                _sessionCreated = true;
            }
        }

        public override void Connected(BoltConnection connection)
        {
            /*
            foreach (LobbyPlayerInfoEntry entry in GetComponentsInChildren<LobbyPlayerInfoEntry>())
            {
                entry.ActivateTeamButton(joinToken.Nickname);
            }
            */
            if (BoltNetwork.IsServer)
            {
                var joinToken = (JoinToken)connection.ConnectToken;
                _serverSidePlayerNicknameList.Add(joinToken.Nickname);
                _lobbyData.PlayersTeamDictionary.Add(joinToken.Nickname, 0);
                SendNicknameListToAllPlayers();
                SendTeamListToAllPlayers();
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                var leaveToken = (JoinToken)connection.ConnectToken;
                _serverSidePlayerNicknameList.Remove(leaveToken.Nickname);
                _lobbyData.PlayersTeamDictionary.Remove(leaveToken.Nickname);
                RemoveEntryFromTeamLists(leaveToken.Nickname);
                SendNicknameListToAllPlayers();
                SendTeamListToAllPlayers();
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
                var team = 0;



                if (_lobbyData.ChosenGamemode == Constants.Gamemodes.FFA)
                {
                    // CycleThroughTeams(evnt.PlayerNickname, evnt.PlayerActualTeam);
                }
                else if (_lobbyData.ChosenGamemode == Constants.Gamemodes.Battle)
                {


                    if (_blueTeamList.ContainsKey(evnt.PlayerNickname))
                    {
                        _blueTeamList.Remove(evnt.PlayerNickname);
                    }
                    else if (_redTeamList.ContainsKey(evnt.PlayerNickname))
                    {
                        _redTeamList.Remove(evnt.PlayerNickname);
                    }

                    if (evnt.PlayerActualTeam.ToTeam() == Team.Red)
                    {
                        team = 0;
                    }
                    else if (evnt.PlayerActualTeam.ToTeam() != Team.Blue)
                    {
                        if (_blueTeamList.Count <= 2)
                        {
                            _blueTeamList.Add(evnt.PlayerNickname, 2);
                            team = 2;
                        }
                        else
                        {
                            _redTeamList.Add(evnt.PlayerNickname, 3);
                            team = 3;
                        }
                    }
                    else if (evnt.PlayerActualTeam.ToTeam() != Team.Red)
                    {
                        if (_redTeamList.Count <= 2)
                        {
                            _redTeamList.Add(evnt.PlayerNickname, 3);
                            team = 3;
                        }
                        else
                        {
                            _blueTeamList.Add(evnt.PlayerNickname, 2);
                            team = 2;
                        }
                    }


                    UpdateTeamColorInLobby updateTeamColorInLobby = UpdateTeamColorInLobby.Create();
                    updateTeamColorInLobby.PlayerNickname = evnt.PlayerNickname;
                    updateTeamColorInLobby.PlayerTeamColor = team;
                    updateTeamColorInLobby.Send();


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
            if (BoltNetwork.IsServer)
            {
                if (_lobbyData.PlayersTeamDictionary.ContainsKey(evnt.PlayerNickname))
                {
                    _lobbyData.PlayersTeamDictionary[evnt.PlayerNickname] = evnt.PlayerTeamColor;
                }
            }

            if (_myPlayerSettings.Nickname == evnt.PlayerNickname)
            {
                _myPlayerSettings.Team = evnt.PlayerTeamColor.ToTeam();
            }
        }

        //PUBLIC

        public void GenerateRandomTeamForNoTeamPlayers()
        {
            foreach (string entry in _serverSidePlayerNicknameList)
            {
                if (!_lobbyData.PlayersTeamDictionary.ContainsKey(entry))
                {
                    TeamColorChangeRequest teamColorChangeRequest = TeamColorChangeRequest.Create();
                    teamColorChangeRequest.PlayerNickname = entry;
                    teamColorChangeRequest.PlayerActualTeam = 0;
                    teamColorChangeRequest.Send();
                    Debug.LogError("RANDOM TEAM GENERATED");
                }
            }
        }

        public void ResetServerList()
        {
            if (BoltNetwork.IsServer)
            {
                _serverSidePlayerNicknameList.Clear();
                _lobbyData.PlayersTeamDictionary.Clear();
            }
        }

        //PRIVATE

        private void RemoveEntryFromTeamLists(string playerNickname)
        {
            if (_blueTeamList.ContainsKey(playerNickname))
            {
                _blueTeamList.Remove(playerNickname);
            }
            else if (_redTeamList.ContainsKey(playerNickname))
            {
                _redTeamList.Remove(playerNickname);
            }
        }

        private void CycleThroughTeams(string nickname, int team)
        {
            if (team == 11 || team == 0)
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

        private void SendTeamListToAllPlayers()
        {
            foreach (string nickname in _blueTeamList.Keys)
            {
                UpdateTeamColorInLobby updateTeamColorInLobby = UpdateTeamColorInLobby.Create();
                updateTeamColorInLobby.PlayerNickname = nickname;
                updateTeamColorInLobby.PlayerTeamColor = _blueTeamList[nickname];
                updateTeamColorInLobby.Send();
            }
            foreach (string nickname in _redTeamList.Keys)
            {
                UpdateTeamColorInLobby updateTeamColorInLobby = UpdateTeamColorInLobby.Create();
                updateTeamColorInLobby.PlayerNickname = nickname;
                updateTeamColorInLobby.PlayerTeamColor = _redTeamList[nickname];
                updateTeamColorInLobby.Send();
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
                if (_lobbyData.ChosenGamemode != Constants.Gamemodes.Battle)
                {
                    entry.GetComponent<LobbyPlayerInfoEntry>().DisableAllTeamButtons();
                }
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
