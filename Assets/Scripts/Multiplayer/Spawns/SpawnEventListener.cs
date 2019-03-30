using UnityEngine;
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

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            PlayerQuit playerQuitEvent = PlayerQuit.Create();
            playerQuitEvent.PlayerID = SWMatchmaking.GetMyBoltId();
            playerQuitEvent.Send();
            Debug.Log("Sent PlayerQuit event.");
        }

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.ConnectionID == SWMatchmaking.GetMyBoltId())
            {
                InstantiateKart(evnt.SpawnPosition, evnt.SpawnRotation, evnt.TeamEnum.ToTeam(), (RoomProtocolToken)evnt.RoomToken);
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

            _playerSettings.ColorSettings = _gameSettings.TeamsListSettings.GetSettings(team);

            myKart.transform.position = spawnPosition;
            myKart.transform.rotation = spawnRotation;
            myKart.GetComponent<BoltEntity>().GetState<IKartState>().Team = team.ToString(); // _playerSettings.ColorSettings.TeamEnum.ToString();
            myKart.GetComponent<BoltEntity>().GetState<IKartState>().OwnerID = SWMatchmaking.GetMyBoltId();

            PlayerReady playerReadyEvent = PlayerReady.Create();
            playerReadyEvent.Nickname = _playerSettings.Nickname;
            playerReadyEvent.PlayerID = SWMatchmaking.GetMyBoltId();
            playerReadyEvent.Team = team.ToString();
            playerReadyEvent.Send();
        }
    }
}
