using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapsSpecifics
{
    [RequireComponent(typeof(Collider))]
    public class ItemDestroyerPortal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer && other.CompareTag(Constants.Tag.ItemCollisionHitBox))
            {

            }
        }
    }
}
