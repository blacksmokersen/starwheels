using UnityEngine;
using Bolt;
using Photon;

namespace Multiplayer
{
    /*
     * Component placed on the kart
     * Finds the SpawnCaller on the map (which is not on the kart) and calls for respawn
     *
     */
    public class SpawnAsker : EntityBehaviour
    {
        [Header("Is Active")]
        [SerializeField] private bool Enabled = false;

        [SerializeField] private PlayerSettings _playerSettings;

        public override void Attached()
        {
            if (entity.attachToken != null)
            {
                var roomToken = (RoomProtocolToken)entity.attachToken;
                Enabled = (roomToken.Gamemode == Constants.Gamemodes.Battle || roomToken.Gamemode == Constants.Gamemodes.FFA);
            }
            else
            {
                Debug.LogError("Couldn't find the attached token to set the knockout mode.");
            }
        }

        public void AskForSpawn()
        {
            if (entity.isOwner && Enabled) // Only owner should be able to call for his spawn
            {
                var spawnCaller = FindObjectOfType<SpawnCaller>();
                if (spawnCaller != null)
                {
                    Debug.Log("Asking for spawn");
                    spawnCaller.CallForSpawn(_playerSettings);
                }
            }
        }
    }
}
