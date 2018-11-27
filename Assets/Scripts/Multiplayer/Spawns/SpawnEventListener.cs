using UnityEngine;
using Bolt;
using Photon;


namespace Multiplayer
{
    public class SpawnEventListener : GlobalEventListener
    {
        [SerializeField] private PlayerSettings _playerSettings;

        [Tooltip("GameModes are : 'Totem' and 'Battle'")]
        [SerializeField] private string _gameMode;

        // CORE

        private void Awake()
        {
            if (!BoltNetwork.isConnected) // Used for In-Editor tests
            {
                BoltLauncher.StartServer();
            }
        }

        // BOLT

        public override void BoltStartDone() // Used for In-Editor tests
        {
            RoomProtocolToken roomProtocolToken = new RoomProtocolToken()
            {
                Gamemode = _gameMode,
                PlayersCount = 1,
                RoomInfo = "Solo"
            };

            InstantiateKart(transform.position, transform.rotation, roomProtocolToken); // Scene specific position
        }

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.ConnectionID == _playerSettings.ConnectionID)
            {
                InstantiateKart(evnt.SpawnPosition, evnt.SpawnRotation, (RoomProtocolToken)evnt.RoomToken);
            }
        }

        // PRIVATE

        private void InstantiateKart(Vector3 spawnPosition, Quaternion spawnRotation, RoomProtocolToken roomProtocolToken)
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

            myKart.transform.position = spawnPosition;
            myKart.transform.rotation = spawnRotation;
        }

        /*
        private IEnumerator AskForRespawnInXSecondsRoutine(float x, Color team, int id)
        {
            yield return new WaitForSeconds(x);
            KartDestroyed kartDestroyedEvent = KartDestroyed.Create();
            kartDestroyedEvent.Team = team;
            kartDestroyedEvent.ConnectionID = id;
            kartDestroyedEvent.Send();
        }
        */
    }
}
