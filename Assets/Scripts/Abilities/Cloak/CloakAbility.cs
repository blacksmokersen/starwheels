using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class CloakAbility : Ability, IControllable
    {
        [Header("Unity Events")]
        public UnityEvent OnOwnerCloackSet;
        public UnityEvent OnOwnerCloackUnset;

        [Header("Meshes and Animation")]
        [SerializeField] private GameObject _cloakEffect;
        [SerializeField] private GameObject[] _kartMeshes;
        [SerializeField] private Animator _animator;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _useCloakSound;
        [SerializeField] private AudioSource _endCloakSound;

        private CloakSettings _cloakSettings;

        private void Awake()
        {
            _cloakSettings = (CloakSettings) abilitySettings;
        }

        // BOLT

        private void Update()
        {
            if (entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();
            }
        }

        public override void Detached()
        {
            StopAllCoroutines();
        }

        // PUBLIC

        public new void Reload()
        {
            CanUseAbility = true;
            StopAllCoroutines();
            UnsetCloack();
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
            _animator.SetTrigger("ActivateCloakEffect");
            StartCoroutine(CloakDuration(_cloakSettings.CloakDuration));
            StartCoroutine(Cooldown());
        }

        // PRIVATE

        private IEnumerator CloakDuration(float Duration)
        {
            yield return new WaitForSeconds(1);
            SetCloack();
            yield return new WaitForSeconds(Duration);
            UnsetCloack();
        }

        private void SendCloakEvent()
        {
            var cloakEvent = CloakAbilityEvent.Create();
            cloakEvent.ActivationBool = CanUseAbility;
            cloakEvent.Entity = entity;
            cloakEvent.Send();
        }

        private void SetCloack()
        {
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useCloakSound);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(false);
            }
            _cloakEffect.SetActive(true);

            if (entity.isOwner)
            {
                OnOwnerCloackSet.Invoke();
            }
        }

        private void UnsetCloack()
        {
            _cloakEffect.SetActive(false);
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_endCloakSound);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(true);
            }

            if (entity.isAttached && entity.isOwner)
            {
                OnOwnerCloackUnset.Invoke();
            }
        }
    }
}
