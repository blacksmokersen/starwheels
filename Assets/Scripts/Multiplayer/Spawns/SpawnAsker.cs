using UnityEngine;

namespace Multiplayer
{
    /*
     * Component placed on the kart
     * Finds the SpawnCaller on the map (which is not on the kart) and calls for respawn
     *
     */
    public class SpawnAsker : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;

        public void AskForSpawn()
        {
            var spawnCaller = FindObjectOfType<SpawnCaller>();
            if(spawnCaller != null)
            {
                spawnCaller.CallForSpawn(_playerSettings);
            }
        }
    }
}
