using Kart;
using UnityEngine;

namespace Items
{
    public class Nitro : MonoBehaviour, IItem
    {
        public int Count { get; set; }

        [Header("Nitro parameters")]
        public float MagnitudeBoost;
        public float SpeedBoost;
        public float BoostDuration;

        public void Stack()
        {
            Debug.Log("Nitro not stackable.");
        }

        public void UseBackward()
        {
            Debug.Log("Nitro not usable backwards.");
        }

        public void UseForward()
        {
            StartCoroutine(GetComponentInParent<KartPhysics>().Boost(BoostDuration, MagnitudeBoost, SpeedBoost));
        }
    }
}
