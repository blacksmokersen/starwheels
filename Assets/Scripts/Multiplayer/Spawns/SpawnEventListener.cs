﻿using UnityEngine;
using Bolt;
using Photon;


namespace Multiplayer
{
    public class SpawnEventListener : GlobalEventListener
    {
        private PlayerSettings _playerSettings;
        private GameSettings _gameSettings;

        [Tooltip("GameModes are : 'Totem' and 'Battle'")]
        [SerializeField] private string _gameMode;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);

            if (!BoltNetwork.IsConnected) // Used for In-Editor tests
            {
                BoltLauncher.StartServer();
            }
        }

        // BOLT

        public override void BoltStartDone() // Used for In-Editor tests
        {
            RoomProtocolToken _roomProtocolToken = new RoomProtocolToken()
            {
                Gamemode = _gameMode,
                PlayersCount = 1,
                RoomInfo = "Solo"
            };
            InstantiateKart(transform.position, transform.rotation, Team.Blue, _roomProtocolToken); // Scene specific position
        }

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.ConnectionID == SWMatchmaking.GetMyBoltId())
            {
                Team teamEnum = (Team) System.Enum.Parse(typeof(Team), evnt.TeamEnum);
                InstantiateKart(evnt.SpawnPosition, evnt.SpawnRotation, teamEnum, (RoomProtocolToken)evnt.RoomToken);
            }
        }

        // PRIVATE

        private void InstantiateKart(Vector3 spawnPosition, Quaternion spawnRotation, Team team, RoomProtocolToken roomProtocolToken)
        {
            GameObject myKart;

            if (roomProtocolToken != null)
            {
                myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, roomProtocolToken);
            }
            else
            {
                Debug.LogError("RoomToken not set.");
                myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            }

            _playerSettings.ColorSettings = _gameSettings.TeamsListSettings.FindSettingsWithTeamEnum(team);

            myKart.transform.position = spawnPosition;
            myKart.transform.rotation = spawnRotation;
            Debug.Log("Team bolt changed : " + _playerSettings.ColorSettings.BoltColor);
            myKart.GetComponent<BoltEntity>().GetState<IKartState>().Team = _playerSettings.ColorSettings.BoltColor;
            myKart.GetComponent<BoltEntity>().GetState<IKartState>().OwnerID = SWMatchmaking.GetMyBoltId();
        }
    }
}
