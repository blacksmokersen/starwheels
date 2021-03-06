﻿using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.UI;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class InGameScoresPanelUpdater : GlobalEventListener
    {
        public Dictionary<Team, TeamInGameScoresEntry> TeamScoreEntries = new Dictionary<Team, TeamInGameScoresEntry>();
        public Dictionary<int, PlayerInGameScoresEntry> PlayerScoreEntries = new Dictionary<int, PlayerInGameScoresEntry>();

        [Header("UI Elements")]
        [SerializeField] private RectTransform _rootPanel;
        [SerializeField] private Transform _teamEntriesParent;

        [Header("Prefabs")]
        [SerializeField] private TeamInGameScoresEntry _teamPrefab;
        [SerializeField] private PlayerInGameScoresEntry _playerPrefab;

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!PlayerScoreEntries.ContainsKey(evnt.PlayerID))
            {
                CreateEntryForPlayer(evnt.PlayerID, evnt.Nickname, evnt.Team.ToTeam());
            }
        }

        public override void OnEvent(PlayerAllStats evnt)
        {
            if (evnt.TargetPlayerID == SWMatchmaking.GetMyBoltId())
            {
                var entry = CreateEntryForPlayer(evnt.PlayerID, evnt.Name, evnt.Team.ToTeam());
                if (entry)
                {
                    entry.UpdateKillCount(evnt.KillCount);
                    entry.UpdateDeathCount(evnt.DeathCount);
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

        public PlayerInGameScoresEntry CreateEntryForPlayer(int id, string nickname, Team team)
        {
            if (!PlayerScoreEntries.ContainsKey(id))
            {
                var entry = Instantiate(_playerPrefab);
                entry.UpdateNickname(nickname);

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

        // PRIVATE

        private bool ParentHasOnlyOneChild(Transform parent)
        {
            var childCount = parent.childCount;
            return childCount <= 2;
        }
    }
}
