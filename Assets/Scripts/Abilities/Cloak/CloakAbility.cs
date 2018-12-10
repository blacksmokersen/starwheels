using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class CloakAbility : Ability, IControllable
    {
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

        public override void SimulateController()
        {
            if (gameObject.activeInHierarchy)
                MapInputs();
        }

        // PUBLIC

        public new void Reload()
        {
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
            Debug.Log("Used cloak");

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
        }

        private void UnsetCloack()
        {
            _cloakEffect.SetActive(false);
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_endCloakSound);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(true);
            }
        }
    }
}
