using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class ExplosionOnPlayerHit : GlobalEventListener
{
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private GameObject _kartMeshes;
    [SerializeField] private GameObject _kartcolliders;

    public override void OnEvent(PlayerHit playerHit)
    {
        var entity = GetComponentInParent<BoltEntity>();

        if (entity == playerHit.PlayerEntity)
        {
            _explosionEffect.SetActive(true);
            _kartMeshes.SetActive(false);
            _kartcolliders.SetActive(false);
        }
    }
}
