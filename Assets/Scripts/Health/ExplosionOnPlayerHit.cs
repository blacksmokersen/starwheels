using UnityEngine;
using Bolt;

namespace Health
{
    public class ExplosionOnPlayerHit : GlobalEventListener
    {
        [SerializeField] private GameObject _explosionEffect;

        public void HideKartMeshAndExplode()
        {
            _explosionEffect.SetActive(true);
            _explosionEffect.GetComponent<SelfDestroyer>().StartCountdown();
            _explosionEffect.transform.SetParent(null);
        }
    }
}
