using UnityEngine;
using System.Collections;
using MyExtensions;

namespace Kart
{
    public class KartHealthSystem : BaseKartComponent
    {
        public int MaxHealth = 3;
        public int Health;
        public float SpamHitSecurity;
        public float HitStopKartDuration;

        private bool isInvincible = false;
        private bool isDead = false;

        private new void Awake()
        {            
            base.Awake();
            Health = MaxHealth;
            kartEvents.OnHit += HealthLoss;
        }

        public void HealthLoss()
        {
            photonView.RPC("RPCHealthLoss", PhotonTargets.All);
        }

        [PunRPC]
        public void RPCHealthLoss()
        {
            if (!isInvincible)
            {
                Health--;
                StartCoroutine(Invicibility(SpamHitSecurity));
                kartEvents.OnHealthLoss(Health);
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
