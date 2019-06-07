using System.Collections.Generic;
using UnityEngine;

namespace Gamemodes
{
    public class BattleServerRules : GamemodeBase
    {
        public IntVariable MaxScore;

        [Header("Settings")]
        [SerializeField] private float _secondsSecurityBetweenHits;

        private Dictionary<int, float> _hitSecurity;

        // CORE

        private void Update()
        {
            foreach (var key in _hitSecurity.Keys)
            {
                _hitSecurity[key] += Time.deltaTime;

                if (_hitSecurity[key] > _secondsSecurityBetweenHits)
                {
                    _hitSecurity.Remove(key);
                }
            }
        }

        // BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            if (BoltNetwork.IsServer && evnt.KillerID != evnt.VictimID && !_hitSecurity.ContainsKey(evnt.KillerID))
            {
                IncreaseScore(evnt.KillerTeam.ToTeam(), 1);
                SendScoreIncreasedEvent(evnt.KillerTeam.ToTeam());
                _hitSecurity.Add(evnt.KillerID, 0f);

                CheckScore();
            }
        }

        // PRIVATE

        private void CheckScore()
        {
            foreach (var entry in scores)
            {
                if (scores[entry.Key] >= MaxScore.Value)
                {
                    WinnerTeam = entry.Key;
                    SendWinningTeamEvent(entry.Key);
                }
            }
        }
    }
}
