using System.Collections;
using UnityEngine;
using System;
using Bolt;

namespace Abilities
{
    public class CloakAbility : AbilitiesBehaviour, IControllable
    {
        [SerializeField] private CloakSettings _cloakSettings;
        [SerializeField] private GameObject _cloakEffect;
        [SerializeField] private GameObject[] _kartMeshes;
        [SerializeField] private Animator _animator;

        public bool CanUseAbility = true;
      //  public BoltEntity Entity;

        public override void SimulateController()
        {
            if (gameObject.activeInHierarchy)
                MapInputs();
        }

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                if (CanUseAbility)
                {
                  //  SendCloakEvent();
                }
            }
        }

        public void Use()
        {
            Debug.Log("used cloak");

            _animator.SetTrigger("ActivateCloakEffect");
            StartCoroutine(CloakDuration(2));
            StartCoroutine(AbilityCooldown(_cloakSettings.CooldownDuration));
        }

        private IEnumerator AbilityCooldown(float Duration)
        {
            CanUseAbility = false;
            yield return new WaitForSeconds(Duration);
            CanUseAbility = true;
        }

        private IEnumerator CloakDuration(float Duration)
        {
            yield return new WaitForSeconds(1);

            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(false);
            }
            // if(GetComponentInParent<BoltEntity>() == entity)
            _cloakEffect.SetActive(true);

            yield return new WaitForSeconds(Duration);

            _cloakEffect.SetActive(false);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(true);
            }
        }
        /*
        public void SendCloakEvent()
        {
            Debug.Log("1");
            var cloakEvent = CloakAbilityEvent.Create();
            cloakEvent.CanUseAbility = CanUseAbility;
            cloakEvent.BoltEntity = entity;
            cloakEvent.Send();
        }
        */
    }
}
