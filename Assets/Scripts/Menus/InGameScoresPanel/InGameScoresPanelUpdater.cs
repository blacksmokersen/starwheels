﻿using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.UI;
using Steamworks;
using System;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class InGameScoresPanelUpdater : GlobalEventListener
    {
        public Dictionary<Team, TeamInGameScoresEntry> TeamScoreEntries = new Dictionary<Team, TeamInGameScoresEntry>();
        public Dictionary<int, PlayerInGameScoresEntry> PlayerScoreEntries = new Dictionary<int, PlayerInGameScoresEntry>();

        [Header("UI Elements")]
        [SerializeField] private InGameScoresPanelDisplayer _displayer;
        [SerializeField] private Transform _teamEntriesParent;

        [Header("Prefabs")]
        [SerializeField] private TeamInGameScoresEntry _teamPrefab;
        [SerializeField] private PlayerInGameScoresEntry _playerPrefab;

        [Header("Stats")]
        [SerializeField] private PlayersStats _playersStats;
        [SerializeField] private TeamsStats _teamsStats;

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!PlayerScoreEntries.ContainsKey(evnt.PlayerID))
            {
                CreateEntryForPlayer(evnt.PlayerID, evnt.Nickname, evnt.Team.ToTeam(), evnt.SteamID);
            }
        }

        public override void OnEvent(PlayerAllStats evnt)
        {
            if (evnt.TargetPlayerID == SWMatchmaking.GetMyBoltId())
            {
                var entry = CreateEntryForPlayer(evnt.PlayerID, evnt.Name, evnt.Team.ToTeam(), evnt.SteamID);
                if (entry)
                {
                    entry.UpdateKillCount(evnt.KillCount);
                    entry.UpdateDeathCount(evnt.DeathCount);
                    entry.UpdateTeamColor(evnt.Team.ToTeam());
                }
            }
        }

        public override void OnEvent(PlayerQuit evnt)
        {
            DestroyEntryForPlayer(evnt.PlayerID);
        }

        public override void Disconnected(BoltConnection connection)
        {
            DestroyEntryForPlayer((int)connection.ConnectionId);
        }

        // PUBLIC

        public PlayerInGameScoresEntry CreateEntryForPlayer(int id, string nickname, Team team, string steamID)
        {
            if (!PlayerScoreEntries.ContainsKey(id))
            {
                var entry = Instantiate(_playerPrefab);
                if (SteamManager.Initialized)
                {
                    entry.SteamID = new CSteamID() { m_SteamID = Convert.ToUInt64(steamID) };
                }
                entry.UpdateAvatar(entry.SteamID);
                entry.UpdateNickname(nickname);
                entry.UpdateTeamColor(team);

                if (!TeamScoreEntries.ContainsKey(team))
                {
                    var teamEntry = Instantiate(_teamPrefab);
                    teamEntry.transform.SetParent(_teamEntriesParent, false);
                    teamEntry.SetTeam(team);
                    teamEntry.SetColorAccordingToTeam();
                    TeamScoreEntries.Add(team, teamEntry);
                }
                entry.transform.SetParent(TeamScoreEntries[team].gameObject.transform, false);

                PlayerScoreEntries.Add(id, entry);
                return entry;
            }
            else
            {
                Debug.LogError("An entry for this player already exists.");
                return null;
            }
        }

        public void DestroyEntryForPlayer(int id)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                var parent = PlayerScoreEntries[id].transform.parent;

                if (parent && ParentHasOnlyOneChild(parent))
                {
                    Destroy(parent.gameObject);
                }
                else
                {
                    Destroy(PlayerScoreEntries[id].gameObject);
                }
            }
            else
            {
                Debug.LogError("Entry couldn't be destroyed since ID was not found.");
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        public void UpdatePlayerKillCount(int id, int killCount)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateKillCount(killCount);
                var team = _playersStats.AllPlayersStats[id].Team;
                UpdateTeamEntryRank(team, TeamScoreEntries[team]);
            }
            else
            {
                Debug.LogError("Provided ID could not be found in the players scores entries.");
            }
        }

        public void UpdatePlayerDeathCount(int id, int deathCount)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateDeathCount(deathCount);
            }
            else
            {
                Debug.LogError("Provided ID could not be found in the players scores entries.");
            }
        }

        public void UpdatePlayerAbility(int id, int abilityIndex)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateAbilityLogo(abilityIndex);
            }
            else
            {
                Debug.LogError("Provided ID could not be found in the players scores entries.");
            }
        }

        public void UpdatePlayerEntryRank(int playerID, PlayerInGameScoresEntry entry)
        {
            var rank = _playersStats.GetPlayerRank(playerID);

            _displayer.ShowPanel();
            entry.transform.SetSiblingIndex(rank - 1);
            _displayer.HidePanel();
        }

        public void UpdateTeamEntryRank(Team team, TeamInGameScoresEntry entry)
        {
            var rank = _teamsStats.GetTeamRank(team);

            _displayer.ShowPanel();
            entry.transform.SetSiblingIndex(rank);
            _displayer.HidePanel();
            Debug.LogError("Updating team entry rank to " + rank);
        }

        // PRIVATE

        private bool ParentHasOnlyOneChild(Transform parent)
        {
            var childCount = parent.childCount;
            return childCount <= 2;
        }
    }
}
