using UnityEngine;

namespace Gamemodes.Totem
{
    [DisallowMultipleComponent]
    public class TotemGutter : MonoBehaviour
    {
        [SerializeField] private Vector3 _respawnPosition;

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.TotemRespawn)) // Physical collider
            {
                if (BoltNetwork.IsServer)
                {
                    foreach (var totemSpawn in FindObjectsOfType<TotemSpawner>())
                    {
                        if (totemSpawn.RespawnSide == TotemSpawner.Side.Center)
                        {
                            totemSpawn.RespawnTotem();
                        }
                    }
                }
                else
                {
                    FindObjectOfType<TotemOwnership>().UnsetOwner();
                }
            }
        }
    }
}
