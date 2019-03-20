using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer;

namespace Totem
{
    public class RespawnerOnGoal : GlobalEventListener
    {
        [Header("Kart to teleport")]
        [SerializeField] private BoltEntity _kartRoot;

        private List<TeamSpawn> _spawns;
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _spawns = new List<TeamSpawn>(FindObjectsOfType<TeamSpawn>());

            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            var scorerTeam = evnt.Team.ToTeam().OppositeTeam();
            if (_kartRoot.isOwner && scorerTeam == _playerSettings.ColorSettings.TeamEnum)
            {
                TeleportOnSpawn();
            }
        }

        // PRIVATE

        private void TeleportOnSpawn()
        {
            var teleportTarget = GetRandomSpawnForTeam(_playerSettings.ColorSettings.TeamEnum);
            _kartRoot.transform.position = teleportTarget.transform.position;
            _kartRoot.transform.rotation = teleportTarget.transform.rotation;
            _kartRoot.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        private GameObject GetRandomSpawnForTeam(Team team)
        {
            var validSpawns = new List<TeamSpawn>();
            foreach (var spawn in _spawns)
            {
                if (spawn.Team == team)
                {
                    validSpawns.Add(spawn);
                }
            }
            return validSpawns[Random.Range(0, validSpawns.Count)].gameObject;
        }
    }
}
