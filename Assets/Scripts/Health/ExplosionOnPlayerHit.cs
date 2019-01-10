using UnityEngine;
using Bolt;

namespace Health
{
    public class ExplosionOnPlayerHit : GlobalEventListener
    {
        public bool Enabled = true;

        [SerializeField] private GameObject _explosionEffect;

        public void InvokeExplosion()
        {
            _explosionEffect.SetActive(true);
            _explosionEffect.transform.SetParent(null);
            _explosionEffect.GetComponent<SelfDestroyer>().StartCountdown();
        }

        private void OnDestroy()
        {
            if (Enabled)
            {
                InvokeExplosion();
            }
        }

        private void OnApplicationQuit()
        {
            Enabled = false;
        }
    }
}
