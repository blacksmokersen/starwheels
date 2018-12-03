using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Abilities
{
    public class CloakEventManager : GlobalEventListener
    {
        [SerializeField] private GameObject _cloakAbilityGO;
        [SerializeField] private CloakAbility _cloakAbility;

        /*
        public override void OnEvent(CloakAbilityEvent evnt)
        {
            var entity = GetComponentInParent<BoltEntity>();
            Debug.LogError("onEventCloak");
            if (entity == evnt.BoltEntity)
            {
                _cloakAbilityGO.SetActive(true);
                Debug.LogError("onEventCloakINIF");
                cloakAbility.CanUseAbility = evnt.CanUseAbility;
             //   cloakAbility.Entity = evnt.BoltEntity;
                cloakAbility.Use();
            }
        }
        */
    }
}
