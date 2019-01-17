using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer;
using Multiplayer.Teams;

namespace Totem
{
    public class RespawnerOnGoal : GlobalEventListener
    {
        [Header("Kart to teleport")]
        [SerializeField] private BoltEntity _kartRoot;

        private List<GameObject> _blueSpawns;
        private List<GameObject> _redSpawns;
        private PlayerSettings _playerSettings;

        private void Awake()
        {
            _blueSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.BlueSpawn));
            _redSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.RedSpawn));

            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        public override void OnEvent(TotemWallHit evnt)
        {
            var scorerColor = evnt.Team.GetTeam().OppositeTeam().GetColor();
            if (_kartRoot.isOwner && scorerColor == _playerSettings.TeamColor)
            {
                TeleportOnSpawn();
            }
        }

        // PRIVATE

        private void TeleportOnSpawn()
        {
            if (_playerSettings.TeamColor == TeamsColors.BlueColor)
            {
                var teleportTarget = _blueSpawns[Random.Range(0, _blueSpawns.Count - 1)];
                _kartRoot.transform.position = teleportTarget.transform.position;
            }
            else if (_playerSettings.TeamColor == TeamsColors.RedColor)
            {
                var teleportTarget = _redSpawns[Random.Range(0, _redSpawns.Count - 1)];
                _kartRoot.transform.position = teleportTarget.transform.position;
            }
            else
            {
                Debug.LogError("Can't teleport because the team is unknown.");
            }

            _kartRoot.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
