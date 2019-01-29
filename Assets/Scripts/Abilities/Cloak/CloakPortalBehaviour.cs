using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace Abilities
{
    public class CloakPortalBehaviour : EntityBehaviour<IItemState>
    {
        [SerializeField] GameObject _targetPortal;

        public void TeleportPlayerToTargetPortal(GameObject kart,GameObject targetPortal)
        {



        }





        private void OnTriggerEnter(Collider other)
        {
            if (entity.isAttached && entity.isOwner && other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                if (other.gameObject.GetComponentInChildren<CloakAbility>().CanUsePortals)
                {
                    TeleportPlayerToTargetPortal(other.gameObject,_targetPortal);
                }
            }
        }
    }
}
