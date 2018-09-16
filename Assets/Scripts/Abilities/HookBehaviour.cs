using UnityEngine;
using Items;

namespace Abilities
{
    public class HookBehaviour : MonoBehaviour
    {
        [HideInInspector] public KartInventory KartInventory;

        private Animator _animator;

        // CORE

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // PRIVATE

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _animator.SetTrigger("Back");
            }

            else if (other.CompareTag(Constants.Tag.DiskItem)
                || other.CompareTag(Constants.Tag.RocketItem)
                || other.CompareTag(Constants.Tag.GuileItem)
                || other.CompareTag(Constants.Tag.GroundItem))
            {
                KartInventory.Item = other.GetComponentInParent<ItemBehaviour>().ItemData;
                KartInventory.Count = 1;
            }

            else if (other.CompareTag(Constants.Tag.KartTrigger))
            {
                var otherKartInventory = other.GetComponentInParent<Kart.KartHub>().kartInventory;
                KartInventory.Item = otherKartInventory.Item;
                KartInventory.Count = otherKartInventory.Count;
            }
        }
    }
}
