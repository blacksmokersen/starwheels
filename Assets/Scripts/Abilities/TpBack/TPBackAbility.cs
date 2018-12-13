using System.Collections;
using UnityEngine;
using Items;
using ThrowingSystem;

namespace Abilities
{
    public class TPBackAbility : Ability, IControllable
    {
        [Header("Launcher")]
        [SerializeField] private ThrowableLauncher _throwableLauncher;

        [Header("Effects")]
        [SerializeField] private AudioSource _useTpBackSound;
        [SerializeField] private GameObject _kartMeshes;

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

        public new void Reload()
        {
            CanUseAbility = true;
            _kartMeshes.SetActive(true);

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
                    _throwableLauncher.Throw(throwable, _throwableLauncher.GetThrowingDirection());
                }
                else // if (_tpBack.IsEnabled())
                {
                    StartCoroutine(BlinkTpBack());
                    StartCoroutine(Cooldown());
                }
            }
        }

        // PRIVATE

        private IEnumerator BlinkTpBack()
        {
            _kartMeshes.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _kartMeshes.SetActive(true);
            var y = _tpBack.transform.position.y + 5f;
            _rb.transform.position = new Vector3(_tpBack.transform.position.x, y, _tpBack.transform.position.z);
            _rb.transform.rotation = GetKartRotation();
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
            Destroy(_tpBack.gameObject);
        }
    }
}
