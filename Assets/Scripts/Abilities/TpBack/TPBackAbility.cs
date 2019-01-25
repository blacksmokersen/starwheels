using System.Collections;
using UnityEngine;
using Items;
using ThrowingSystem;
using UnityEngine.Events;

namespace Abilities
{
    public class TPBackAbility : Ability, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Unity Events")]
        public UnityEvent OnBlinkActivated;

        [Header("Launcher")]
        [SerializeField] private ThrowableLauncher _throwableLauncher;
        [SerializeField] private ThrowingDirection _throwingDirection;

        [Header("Effects")]
        [SerializeField] private AudioSource _useTpBackSound;
        [SerializeField] private GameObject _kartMeshes;

        [Header("Invicibility")]
        [SerializeField] private Health.Health _health;

        private TPBackSettings _tPBackSettings;
        private ParticleSystem _reloadEffect;
        private TPBackBehaviour _tpBack = null;
        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _tPBackSettings = (TPBackSettings) abilitySettings;
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
            {
                Use();
            }
        }

        public new void Reload()
        {
            CanUseAbility = true;
            _kartMeshes.SetActive(true);
            _health.UnsetInvincibility();

            if (_tpBack)
            {
                MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
                Destroy(_tpBack.gameObject);
            }
        }

        public Quaternion GetKartRotation()
        {
            return _rb.transform.rotation;
        }

        public void Use()
        {
            if (CanUseAbility)
            {
                if (_tpBack == null)
                {
                    var instantiatedItem = BoltNetwork.Instantiate(_tPBackSettings.Prefab);

                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _tpBack = instantiatedItem.GetComponent<TPBackBehaviour>();
                    _throwableLauncher.Throw(throwable, _throwingDirection.CurrentDirection);
                }
                else // if (_tpBack.IsEnabled())
                {
                    StartCoroutine(Cooldown());
                    StartCoroutine(BlinkTpBack());
                }
            }
        }

        // PRIVATE

        private IEnumerator BlinkTpBack()
        {
            OnBlinkActivated.Invoke();
            _kartMeshes.SetActive(false);
            _health.SetInvincibility();
            yield return new WaitForSeconds(0.5f);
            _kartMeshes.SetActive(true);
            _health.UnsetInvincibility();
            var y = _tpBack.transform.position.y + 5f;
            _rb.transform.position = new Vector3(_tpBack.transform.position.x, y, _tpBack.transform.position.z);
            _rb.transform.rotation = GetKartRotation();
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
            Destroy(_tpBack.gameObject);
        }
    }
}
