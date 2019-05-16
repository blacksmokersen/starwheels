using System.Collections;
using UnityEngine;
using Items;

namespace Abilities
{
    public class WallPrefabScript : MonoBehaviour
    {
        [SerializeField] private WallSettings _wallSettings;

        //CORE

        private void Start()
        {
            StartCoroutine(SelfDestruct(_wallSettings.WallDuration));
        }

        //PRIVATE

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.ItemCollisionHitBox)) // It is an item collision
                {
                    if (other.GetComponentInParent<NetworkDestroyable>())
                    {
                        var itemNetworkDestroyable = other.GetComponentInParent<NetworkDestroyable>();
                        itemNetworkDestroyable.DestroyObject();
                    }

                    StopAllCoroutines();
                    DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                    destroyEntityEvent.Entity = GetComponent<BoltEntity>();
                    destroyEntityEvent.Send();
                }
            }
        }

        IEnumerator SelfDestruct(float wallDuration)
        {
            yield return new WaitForSeconds(wallDuration);
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = GetComponent<BoltEntity>();
            destroyEntityEvent.Send();

        }
    }

}
