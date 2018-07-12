using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kart
{
    public class KartHealthSystem : MonoBehaviour
    {
        public int Health = 3;

        private KartEffects kartEffects;
        private KartOrientation KartOrientation;

        private void Awake()
        {
            kartEffects = FindObjectOfType<KartEffects>();
            KartOrientation = FindObjectOfType<KartOrientation>();
        }

        public void HealtLoss()
        {
            Health--;
            KartOrientation.LooseHealth(2.5f);
            kartEffects.HealthParticlesManagement(Health);
        }
    }
}
