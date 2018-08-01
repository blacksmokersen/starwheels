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

        private bool isInvincible = false;
        private bool isDead = false;
        private KartEvents kartEvents;

        private void Awake()
        {
            Health = MaxHealth;
            kartEvents = GetComponentInParent<KartEvents>();
            kartEvents.OnHit += HealthLoss;
        }

        public void HealthLoss()
        {
            this.ExecuteRPC(PhotonTargets.All, "RPCHealthLoss");
        }

        [PunRPC]
        public void RPCHealthLoss()
        {
            if (!isInvincible)
            {
                Health--;
                StartCoroutine(Invicibility(SpamHitSecurity));
            }
            if(Health <= 0 && !isDead)
            {
                GetComponentInParent<Rigidbody>().transform.position = new Vector3(-221, 3, 0);
                isDead = true;                
            }
        }

        public void ResetLives()
        {
            Health = MaxHealth;
        }

        IEnumerator Invicibility(float invicibilityDuration)
        {
            isInvincible = true;
            yield return new WaitForSeconds(invicibilityDuration);
            isInvincible = false;
        }
    }
}
