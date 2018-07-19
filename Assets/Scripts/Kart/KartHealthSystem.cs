using UnityEngine;
using System.Collections;

namespace Kart
{
    public class KartHealthSystem : MonoBehaviour
    {
        public int Health = 3;
        public float SpamHitSecurity;
        public float HitStopKartDuration;


        private KartEffects kartEffects;
        private KartOrientation KartOrientation;
        private bool invicibility = false;

        private void Awake()
        {
            kartEffects = GetComponentInChildren<KartEffects>();
            KartOrientation = GetComponent<KartOrientation>();
        }

        public void HealtLoss()
        {
            if (!invicibility)
            {
                Health--;
                KartOrientation.LooseHealth(HitStopKartDuration);
                kartEffects.HealthParticlesManagement(Health);
                StartCoroutine(Invicibility(SpamHitSecurity));
            }
            if(Health <= 0)
            {

                GetComponentInParent<Rigidbody>().transform.position = new Vector3(-221, 3, 0);
            }
        }
        IEnumerator Invicibility(float invicibilityTimer)
        {
            invicibility = true;
            yield return new WaitForSeconds(invicibilityTimer);
            invicibility = false;
        }
    }
}
