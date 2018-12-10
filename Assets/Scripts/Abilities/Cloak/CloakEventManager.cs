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

        public override void OnEvent(CloakAbilityEvent evnt)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == evnt.Entity)
            {
                _cloakAbilityGO.SetActive(true);
                _cloakAbility.CanUseAbility = evnt.ActivationBool;
                _cloakAbility.Use();
            }
        }
    }
}
