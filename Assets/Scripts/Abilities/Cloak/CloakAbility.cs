using System.Collections;
using UnityEngine;

namespace Abilities {
    public class CloakAbility : AbilitiesBehaviour, IControllable
    {
        [SerializeField] private CloakSettings _cloakSettings;
        [SerializeField] private GameObject _cloakEffect;
        [SerializeField] private GameObject[] _kartMeshes;
        [SerializeField] private Animator _animator;

        private bool _canUseAbility = true;


        private void Awake()
        {

        }

        public override void SimulateController()
        {
            if (gameObject.activeInHierarchy)
                MapInputs();
        }

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                Use();
            }
        }


        private void Use()
        {
            if (_canUseAbility)
            {
                Debug.Log("used cloak");

                _animator.SetTrigger("ActivateCloakEffect");
                StartCoroutine(CloakDuration(2));
                StartCoroutine(AbilityCooldown(_cloakSettings.CooldownDuration));
            }
        }

        private IEnumerator AbilityCooldown(float Duration)
        {
            _canUseAbility = false;
            yield return new WaitForSeconds(Duration);
            _canUseAbility = true;
        }

        private IEnumerator CloakDuration(float Duration)
        {
            yield return new WaitForSeconds(1);

            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(false);
            }
            _cloakEffect.SetActive(true);

            yield return new WaitForSeconds(Duration);

            _cloakEffect.SetActive(false);
            foreach (GameObject mesh in _kartMeshes)
            {
                mesh.SetActive(true);
            }
        }
    }
}
