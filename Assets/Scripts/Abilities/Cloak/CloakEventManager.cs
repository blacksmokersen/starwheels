using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Abilities
{
    public class CloakEventManager : GlobalEventListener
    {
        [SerializeField] CloakAbility cloakAbility;
        /*
        public override void OnEvent(CloakAbilityEvent evnt)
        {
            var entity = GetComponentInParent<BoltEntity>();
            Debug.LogError("onEventCloak");
            if (entity == evnt.BoltEntity)
            {
                Debug.LogError("onEventCloakINIF");
                cloakAbility.CanUseAbility = evnt.CanUseAbility;
             //   cloakAbility.Entity = evnt.BoltEntity;
                cloakAbility.Use();
            }
        }
        */
    }
}
