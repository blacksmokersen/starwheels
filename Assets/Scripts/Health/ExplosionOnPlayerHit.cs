using UnityEngine;
using Bolt;

namespace Health
{
    public class ExplosionOnPlayerHit : GlobalEventListener
    {
        [SerializeField] private GameObject _explosionEffect;
        [SerializeField] private GameObject _kartMeshes;
        [SerializeField] private GameObject _kartcolliders;

        public void HideKartMeshAndExplode()
        {
            _kartMeshes.SetActive(false);
            _kartcolliders.SetActive(false);
            _explosionEffect.SetActive(true);
        }
    }
}
