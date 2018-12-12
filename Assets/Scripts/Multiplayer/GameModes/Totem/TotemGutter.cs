using UnityEngine;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemGutter : MonoBehaviour
    {
        [SerializeField] private Vector3 _respawnPosition;

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Totem) && !other.isTrigger) // Physical collider
            {
                if (BoltNetwork.isServer)
                {
                    FindObjectOfType<TotemSpawner>().RespawnTotem();
                }
                else
                {
                    FindObjectOfType<Totem>().UnsetParent();
                }
            }
        }
    }
}
