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
     [DisallowMultipleComponent]
    public class SpawnAsker : EntityBehaviour
    {
        [Header("Is Active (Debug)")]
        [SerializeField] private bool Enabled = false;

        [SerializeField] private PlayerSettings _playerSettings;

        // PUBLIC

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void AskForSpawn()
        {
            if (entity.IsOwner && Enabled) // Only owner should be able to call for his spawn
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
