using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWExtensions;
using Multiplayer;
using Gamemodes;
using Bolt;
using Controls;
using Steamworks;

public class TeamBattlePlayersObserver : GlobalEventListener
{
    private TeamBattleServerRules _teamBattleServerRules;

    private Dictionary<int, int> _playersLifeCount = new Dictionary<int, int>();
    private Dictionary<int, Team> _playersInJail = new Dictionary<int, Team>();
    private Dictionary<int, Team> _deadPlayers = new Dictionary<int, Team>();
    private Dictionary<int, string> _playerSteamID = new Dictionary<int, string>();

    //CORE

    private void Awake()
    {
        _teamBattleServerRules = GetComponent<TeamBattleServerRules>();
    }

    private void Start()
    {
        var hostID = SWExtensions.KartExtensions.GetMyKart().GetComponent<PlayerInfo>().OwnerID;
        if (SteamManager.Initialized)
        {
            var steamID = "" + SteamUser.GetSteamID().m_SteamID;

            if (!_playerSteamID.ContainsKey(hostID))
            {
                Debug.LogError("STEAM ID PlayerAllStats : " + hostID + "  " + steamID);
                _playerSteamID.Add(hostID, steamID);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            foreach (int player in _playersLifeCount.Keys)
            {
                Debug.LogError("- Player ID : " + player + " - PlayerLifeCount : " + _playersLifeCount[player]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            foreach (int player in _playersInJail.Keys)
            {
                Debug.LogError("- Player ID : " + player + " - Team : " + _playersInJail[player]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            DecreasePlayerHealth(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddObservedPlayer(0);
            CheckPlayerHealth(0);
        }
    }

    //BOLT

    public override void OnEvent(PlayerReady evnt)
    {
        if (BoltNetwork.IsServer)
        {
            Debug.LogError("PLAYERREADY");
            if (!_playerSteamID.ContainsKey(evnt.PlayerID))
            {
                Debug.LogError("STEAM ID PlayerAllStats : " + evnt.SteamID);
                _playerSteamID.Add(evnt.PlayerID, evnt.SteamID);
            }
        }
    }

    public override void OnEvent(PlayerAllStats evnt)
    {
        if (BoltNetwork.IsServer)
        {
            Debug.LogError("PLAYERREADY");
            if (!_playerSteamID.ContainsKey(evnt.PlayerID))
            {
                Debug.LogError("STEAM ID PlayerAllStats : " + evnt.SteamID);
                _playerSteamID.Add(evnt.PlayerID, evnt.SteamID);
            }
        }
    }

    //PUBLIC

    public void SendInfoToDisplayOnHUD()
    {
        if (BoltNetwork.IsServer)
        {
            foreach (int playerID in _playersLifeCount.Keys)
            {
                GameObject playerKart = KartExtensions.GetKartWithID(playerID);

                ShareTeamBattlePortraitInfos shareTeamBattlePortraitInfos = ShareTeamBattlePortraitInfos.Create();
                shareTeamBattlePortraitInfos.playerID = playerID;
                shareTeamBattlePortraitInfos.LifeCount = _playersLifeCount[playerID];

                if (SteamManager.Initialized)
                {
                    shareTeamBattlePortraitInfos.SteamID = _playerSteamID[playerID];
                }
                if (_playersInJail.ContainsKey(playerID))
                {
                    shareTeamBattlePortraitInfos.IsInJail = true;
                }
                else
                {
                    shareTeamBattlePortraitInfos.IsInJail = false;
                }
                shareTeamBattlePortraitInfos.Send();
            }

            foreach (int playerID in _deadPlayers.Keys)
            {
                ShareTeamBattlePortraitInfos shareTeamBattlePortraitInfos = ShareTeamBattlePortraitInfos.Create();
                shareTeamBattlePortraitInfos.playerID = playerID;
                shareTeamBattlePortraitInfos.LifeCount = 0;
                shareTeamBattlePortraitInfos.IsDead = true;
                shareTeamBattlePortraitInfos.Send();
                Debug.LogError("KILLL REQUEST");
            }
        }
    }

    public void DecreasePlayerHealth(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            _playersLifeCount[playerID]--;
            Debug.LogError("Decreased : " + playerID + " PlayerLifeCount");
            CheckPlayerHealth(playerID);
        }
    }

    public void AddObservedPlayer(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            if (!_playersLifeCount.ContainsKey(playerID))
            {
                Debug.LogError("Observe Player : " + playerID);
                _playersLifeCount.Add(playerID, _teamBattleServerRules.TeamBattleSettings.LifeCountPerPlayers);

                StartCoroutine(DelayedSendInfoToDisplayOnHUD());
                // SendInfoToDisplayOnHUD();
                /*
                ShareTeamBattlePortraitInfos shareTeamBattlePortraitInfos = ShareTeamBattlePortraitInfos.Create();
                shareTeamBattlePortraitInfos.playerID = playerID;
                shareTeamBattlePortraitInfos.LifeCount = 5;
                shareTeamBattlePortraitInfos.AddPlayer = true;
                shareTeamBattlePortraitInfos.Send();
                */
            }
        }
    }

    public void RemoveObservedPlayer(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            if (_playersLifeCount.ContainsKey(playerID))
            {
                if (_playersLifeCount[playerID] == -1)
                {
                    SendInfoToDisplayOnHUD();
                    _playersLifeCount.Remove(playerID);
                }
                else
                {
                    _playersLifeCount.Remove(playerID);
                    ShareTeamBattlePortraitInfos shareTeamBattlePortraitInfos = ShareTeamBattlePortraitInfos.Create();
                    shareTeamBattlePortraitInfos.playerID = playerID;
                    shareTeamBattlePortraitInfos.RemovePlayer = true;
                    shareTeamBattlePortraitInfos.Send();
                }
            }
        }
    }

    public void RefreshAllKartsInGame()
    {
        if (BoltNetwork.IsServer)
        {
            _playersLifeCount.Clear();
            foreach (GameObject player in SWExtensions.KartExtensions.GetAllKarts())
            {
                int playerID = player.GetComponent<PlayerInfo>().OwnerID;
                _playersLifeCount.Add(playerID, _teamBattleServerRules.TeamBattleSettings.LifeCountPerPlayers);
                Debug.LogError("Observe Player With RefreshAllKartsInGame : " + playerID);
                SendInfoToDisplayOnHUD();
            }
        }
    }

    public void CheckAllKarts()
    {
        if (BoltNetwork.IsServer)
        {
            foreach (int player in _playersLifeCount.Keys)
            {
                if (KartExtensions.GetKartWithID(player) == null)
                {
                    _playersLifeCount.Remove(player);
                    Debug.LogError("REMOVED : " + player);
                }
            }
        }
    }

    public List<int> GetAlivePlayers()
    {
        List<int> alivePlayers = new List<int>();

        foreach (int player in _playersLifeCount.Keys)
        {
            if (_playersInJail.ContainsKey(player))
            {
                Debug.LogError(player + " Is in Jail");
            }
            else if (_playersLifeCount[player] >= 0)
            {
                alivePlayers.Add(player);
            }
        }
        return alivePlayers;
    }

    public void AddPlayerToClientJailList(int playerID)
    {
        if (BoltNetwork.IsClient)
        {
            _playersInJail.Add(playerID, KartExtensions.GetKartWithID(playerID).GetComponent<PlayerInfo>().Team);
        }
    }

    public void FreePlayersFromJail(string jailTeam)
    {
        var playerToRemoveFromList = new List<int>();

        foreach (int player in _playersInJail.Keys)
        {
            if (_playersInJail[player].ToString() != jailTeam)
            {
                GameObject kart = KartExtensions.GetKartWithID(player);
                kart.GetComponentInChildren<Health.Health>().StopHealthCoroutines();
                kart.GetComponentInChildren<Health.Health>().UnsetInvincibility();
                kart.GetComponentInChildren<KartInputManager>().EnableKartInputsInGame();


                playerToRemoveFromList.Add(player);
            }
        }
        foreach (int player in playerToRemoveFromList)
        {
            _playersInJail.Remove(player);
            SendInfoToDisplayOnHUD();
        }
    }

    //PRIVATE

    private void CheckPlayerHealth(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            if (_playersLifeCount[playerID] == 0)
            {
                if (!_playersInJail.ContainsKey(playerID))
                {
                    Debug.LogError("SEND JAIL EVENT : " + playerID);
                    _playersInJail.Add(playerID, KartExtensions.GetKartWithID(playerID).GetComponent<PlayerInfo>().Team);

                    KartForcedToJail kartForcedtoJailEvent = KartForcedToJail.Create();
                    kartForcedtoJailEvent.PlayerID = playerID;
                    kartForcedtoJailEvent.Team = KartExtensions.GetKartWithID(playerID).GetComponent<PlayerInfo>().Team.ToString();
                    kartForcedtoJailEvent.Send();
                }
            }
            else if (_playersLifeCount[playerID] == -1)
            {
                RemoveObservedPlayer(playerID);

                GameObject playerKart = KartExtensions.GetKartWithID(playerID);

                PermanentDeath permanentdeath = PermanentDeath.Create();
                permanentdeath.PlayerEntity = playerKart.GetComponent<BoltEntity>();
                permanentdeath.TimeBeforeDeath = 0;
                permanentdeath.PlayerID = playerID;
                permanentdeath.PlayerTeam = playerKart.GetComponent<PlayerInfo>().Team.ToString();
                permanentdeath.PlayerTeam = playerKart.GetComponent<PlayerInfo>().Nickname;
                permanentdeath.Send();

                _deadPlayers.Add(playerID, playerKart.GetComponent<PlayerInfo>().Team);
                /*
                ShareTeamBattlePortraitInfos shareTeamBattlePortraitInfos = ShareTeamBattlePortraitInfos.Create();
                shareTeamBattlePortraitInfos.playerID = playerID;
                shareTeamBattlePortraitInfos.LifeCount = 0;
                shareTeamBattlePortraitInfos.IsDead = true;
                shareTeamBattlePortraitInfos.Send();
                */
            }
            //   if (_playersLifeCount.ContainsKey(playerID))
            //  {
            SendInfoToDisplayOnHUD();
            //   }
        }
    }

    private IEnumerator DelayedSendInfoToDisplayOnHUD()
    {
        yield return new WaitForSeconds(5);
        SendInfoToDisplayOnHUD();
    }
}
