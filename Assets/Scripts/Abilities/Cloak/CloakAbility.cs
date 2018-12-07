using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class CloakAbility : Ability, IControllable
    {
        [SerializeField] private CloakSettings _cloakSettings;
        [SerializeField] private GameObject _cloakEffect;
        [SerializeField] private GameObject[] _kartMeshes;
        [SerializeField] private Animator _animator;

        [SerializeField] private AudioSource _useCloakSound;
        [SerializeField] private AudioSource _endCloakSound;

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
                    SendCloakEvent();
                }
            }
        }

        public void Use()
        {
            Debug.Log("Used cloak");

            _animator.SetTrigger("ActivateCloakEffect");
            StartCoroutine(CloakDuration(_cloakSettings.CloakDuration));
            StartCoroutine(Cooldown());
        }

        private IEnumerator CloakDuration(float Duration)
        {
            yield return new WaitForSeconds(1);
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useCloakSound);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(false);
            }
            _cloakEffect.SetActive(true);

            yield return new WaitForSeconds(Duration);

            _cloakEffect.SetActive(false);
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_endCloakSound);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(true);
            }
        }

        public void SendCloakEvent()
        {
            var cloakEvent = CloakAbilityEvent.Create();
            cloakEvent.ActivationBool = CanUseAbility;
            cloakEvent.Entity = entity;
            cloakEvent.Send();
        }
    }
}
