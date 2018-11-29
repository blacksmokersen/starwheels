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

        [SerializeField] private TPBackSettings _tPBackSettings;
        [SerializeField] private ThrowableLauncher _throwableLauncher;

        [Header("Effects")]
        [SerializeField] private ParticleSystem _reloadParticlePrefab;
        [SerializeField] private int _reloadParticleNumber;
        [SerializeField] private AudioSource _useTpBackSound;

        private ParticleSystem _reloadEffect;
        private TPBackBehaviour _tpBack = null;
        private bool _canUseAbility = true;
        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        // BOLT

        public override void SimulateController()
        {
            if (gameObject.activeInHierarchy)
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
                    var instantiatedItem = BoltNetwork.Instantiate(_tPBackSettings.Prefab);

                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _tpBack = instantiatedItem.GetComponent<TPBackBehaviour>();
                    _throwableLauncher.Throw(throwable, _throwableLauncher.GetThrowingDirection());
                }
                else if (_tpBack.IsEnabled())
                {
                    _rb.transform.position = _tpBack.transform.position;
                    _rb.transform.rotation = GetKartRotation();
                    MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
                    Destroy(_tpBack.gameObject);
                    StartCoroutine(AbilityCooldown(_tPBackSettings.Cooldown));
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
            _reloadParticlePrefab.Emit(_reloadParticleNumber);
            // OnTpBackEvent.Invoke();
        }
    }
}
