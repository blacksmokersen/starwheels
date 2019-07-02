using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class KartWallDetector : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _inventory.IsWallDetected = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _inventory.IsWallDetected = false;
            }
        }
    }
}
