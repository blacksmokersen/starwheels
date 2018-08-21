using UnityEngine;
using System.Collections;

namespace Kart
{
    public class KartHealthSystem : BaseKartComponent
    {
        public int MaxHealth = 3;
        public int Health;
        public float CrashInvincibilityDuration = 3f;
        public bool IsInvincible = false;        
        public bool IsDead = false;

        private new void Awake()
        {            
            base.Awake();
            Health = MaxHealth;
            kartEvents.OnHit += () => 
            {                
                HealthLoss();
                StartCoroutine(InvicibilityTime());
            };
        }

        public void HealthLoss()
        {            
            photonView.RPC("RPCHealthLoss", PhotonTargets.All);
        }

        [PunRPC]
        public void RPCHealthLoss()
        {
            if (!IsInvincible)
            {                
                Health--;
                kartEvents.OnHealthLoss(Health);
            }
            if(Health <= 0 && !IsDead)
            {
                GetComponentInParent<Rigidbody>().transform.position = new Vector3(-221, 3, 0);
                IsDead = true;                
            }
        }

        public void ResetLives()
        {
            Health = MaxHealth;
        }

        IEnumerator InvicibilityTime()
        {
            IsInvincible = true;
            yield return new WaitForSeconds(CrashInvincibilityDuration);
            IsInvincible = false;
        }
    }
}
