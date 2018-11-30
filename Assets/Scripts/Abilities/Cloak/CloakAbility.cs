using System.Collections;
using UnityEngine;

namespace Abilities {
    public class CloakAbility : AbilitiesBehaviour, IControllable
    {
        [SerializeField] private CloakSettings _cloakSettings;
        [SerializeField] private GameObject _cloakEffect;
        [SerializeField] private GameObject _kartMeshes;
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
              //  _cloakEffect.SetActive(true);
                _animator.SetTrigger("ActivateCloakEffect");
                StartCoroutine(AbilityCooldown(_cloakSettings.CooldownDuration));
            }
        }

        private IEnumerator AbilityCooldown(float Duration)
        {
            _canUseAbility = false;
            yield return new WaitForSeconds(Duration);
            _canUseAbility = true;
        }
    }
}
