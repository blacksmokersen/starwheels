using UnityEngine;
using Common.PhysicsUtils;
using SWExtensions;

namespace MapsSpecifics
{
    [DisallowMultipleComponent]
    public class KartBoostZone : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private BoostSettings _boostSettings;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartCollider))
            {
                other.GetKartRoot().GetComponentInChildren<Boost>().CustomBoostFromBoostSettings(_boostSettings);
            }
        }
    }
}
