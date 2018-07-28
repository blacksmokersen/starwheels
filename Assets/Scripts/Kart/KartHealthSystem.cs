using UnityEngine;
using System.Collections;
using Photon;
using MyExtensions;

namespace Kart
{
    public class KartHealthSystem : PunBehaviour
    {
        public int MaxHealth = 3;
        public int Health;
        public float SpamHitSecurity;
        public float HitStopKartDuration;

        private KartEffects kartEffects;
        private KartOrientation KartOrientation;
        private KartSoundsScript kartSoundsScript;
        private bool invicibility = false;
        public bool dead;

        private void Awake()
        {
            kartEffects = GetComponentInChildren<KartEffects>();
            KartOrientation = GetComponent<KartOrientation>();
            kartSoundsScript = FindObjectOfType<KartSoundsScript>();

            Health = MaxHealth;
        }

        public void HealthLoss()
        {
            this.ExecuteRPC(PhotonTargets.All, "RPCHealthLoss");
        }

        [PunRPC]
        public void RPCHealthLoss()
        {
            if (!invicibility)
            {
                Health--;
                kartSoundsScript.Playerhit();
                KartOrientation.LooseHealth(HitStopKartDuration);
                kartEffects.HealthParticlesManagement(Health);
                StartCoroutine(Invicibility(SpamHitSecurity));
            }
            if(Health <= 0 && !dead)
            {
                GetComponentInParent<Rigidbody>().transform.position = new Vector3(-221, 3, 0);
                dead = true;                
            }
        }

        public void ResetLives()
        {
            Health = MaxHealth;
            kartEffects.ResetLife();
        }

        IEnumerator Invicibility(float invicibilityDuration)
        {
            invicibility = true;
            yield return new WaitForSeconds(invicibilityDuration);
            invicibility = false;
        }
    }
}
