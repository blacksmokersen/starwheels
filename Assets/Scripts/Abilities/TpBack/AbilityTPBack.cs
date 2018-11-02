using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Items;
using ThrowingSystem;

namespace Abilities
{
    public class AbilityTPBack : AbilitiesBehaviour, IControllable
    {
        public UnityEvent OnTpBackEvent;

        [SerializeField] private TPBackSettings tPBackSettings;
        [SerializeField] private ThrowableLauncher throwableLauncher;
        [Header("Effects")]
        [SerializeField] private ParticleSystem reloadParticlePrefab;
        [SerializeField] private int reloadParticleNumber;

        private ParticleSystem _reloadEffect;
        private TPBackBehaviour _tpBack = null;
        private bool _canUseAbility = true;
        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            /*
            var instantiatedReloadEffect = BoltNetwork.Instantiate(tPBackSettings.ReloadParticlePrefab);
            instantiatedReloadEffect.transform.parent = gameObject.transform;
            instantiatedReloadEffect.transform.position = new Vector3(0, 0, 0);
            _reloadEffect = instantiatedReloadEffect.GetComponent<ParticleSystem>();
            */
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

        public Quaternion GetKartRotation()
        {
            return _rb.transform.rotation;
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
                    _rb.transform.rotation = GetKartRotation();
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
            reloadParticlePrefab.Emit(reloadParticleNumber);
            // OnTpBackEvent.Invoke();
        }
    }
}
