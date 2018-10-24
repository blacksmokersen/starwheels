using Items;
using UnityEngine;
using Bolt;
using UnityEngine.Events;
using System.Collections;

namespace Abilities
{
    public class AbilityTPBack : EntityBehaviour<IKartState>, IControllable
    {
        public UnityEvent OnTpBackEvent;

        [SerializeField] private TPBackSettings tPBackSettings;
        [SerializeField] private AbilitiesBehaviourSettings abilitiesBehaviourSettings;
        [SerializeField] private ThrowableLauncher throwableLauncher;

        private ParticleSystem _reloadEffect;
        private TPBackBehaviour _tpBack = null;
        private bool _canUseAbility = true;
        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            var instantiatedReloadEffect = BoltNetwork.Instantiate(tPBackSettings.ReloadParticlePrefab);
            instantiatedReloadEffect.transform.parent = gameObject.transform;
            instantiatedReloadEffect.transform.position = new Vector3(0, 0, 0);
            _reloadEffect = instantiatedReloadEffect.GetComponent<ParticleSystem>();
        }

        // BOLT

        public override void SimulateController()
        {
            if (abilitiesBehaviourSettings.ActiveAbility == "TPBack")
                MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                Use();
            }
        }

        public void Use()
        {
            if (_canUseAbility)
            {
                if (_tpBack == null)
                {
                    var instantiatedItem = BoltNetwork.Instantiate(tPBackSettings.Prefab);
                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _tpBack = instantiatedItem.GetComponent<TPBackBehaviour>();
                    throwableLauncher.Throw(throwable);
                }
                else //if (_tpBack.IsEnabled())
                {
                    _rb.transform.position = _tpBack.transform.position;
                    _rb.transform.rotation = _tpBack.GetKartRotation();
                    Destroy(_tpBack.gameObject);
                    StartCoroutine(AbilityCooldown(tPBackSettings.Cooldown));
                }
            }
        }
        // PRIVATE
        IEnumerator AbilityCooldown(float Duration)
        {
            _canUseAbility = false;
            yield return new WaitForSeconds(Duration);
            _canUseAbility = true;
          //  _reloadEffect.Emit(abilitySettings.ReloadParticleNumber);
            _reloadEffect.Emit(100);
            // OnTpBackEvent.Invoke();
        }
    }
}
