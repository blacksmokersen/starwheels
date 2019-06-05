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
        private bool _buttonPressed;

        // CORE

        private void Awake()
        {
            _tPBackSettings = (TPBackSettings)abilitySettings;
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
                _buttonPressed = true;
                CheckDirection();
            }
            if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility) && _buttonPressed)
            {
                Use();
                _buttonPressed = false;
            }




            /*
            if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility) && Input.GetButton(Constants.Input.UseAbilityBackward))
            {
                Use(Direction.Forward);
            }
            else if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility) && Input.GetButton(Constants.Input.UseAbilityForward))
            {
                Use(Direction.Backward);
            }
            else if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility))
            {
                Use(_throwingDirection.CurrentDirection);
            }
            */
        }

        public new void Reload()
        {
            CanUseAbility = true;
            _kartMeshes.SetActive(true);
            _health.UnsetInvincibility();

            if (_tpBack)
            {
                SWExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
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
                    _tpBack.Kart = transform.root;
                    /*
                    if (direction == Direction.Backward)
                        _throwableLauncher.Throw(throwable, Direction.Backward);
                    else
                        _throwableLauncher.Throw(throwable, Direction.Forward);
                        */
                }
                else // if (_tpBack.IsEnabled())
                {
                    StartCoroutine(Cooldown());
                    StartCoroutine(BlinkTpBack());
                }
            }
        }

        // PRIVATE

        private Direction CheckDirection()
        {
            var leftJoytstickInput = Input.GetAxis(Constants.Input.UpAndDownAxis);
            Debug.Log(leftJoytstickInput);

            if (Input.GetButton(Constants.Input.UseItemBackward))
                return Direction.Forward;
            else
                return Direction.Backward;
        }

        private IEnumerator BlinkTpBack()
        {
            OnBlinkActivated.Invoke();
            _kartMeshes.SetActive(false);
            _health.SetInvincibility();
            yield return new WaitForSeconds(0.25f);
            _kartMeshes.SetActive(true);
            _health.UnsetInvincibility();
            SWExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
            if (_tpBack != null)
            {
                var y = _tpBack.transform.position.y + _tPBackSettings.IncreasedYPositionOnTP;
                _rb.transform.position = new Vector3(_tpBack.transform.position.x, y, _tpBack.transform.position.z);
                _rb.transform.rotation = GetKartRotation();
                Destroy(_tpBack.gameObject);
            }
        }
    }

    /*
     * using System.Collections;
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
        private Direction _keyboardDirection;

        // CORE

        private void Awake()
        {
            _tPBackSettings = (TPBackSettings)abilitySettings;
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
            if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility))
                Use();
            if (Input.GetButtonDown(Constants.Input.UseItemBackward) &&_tpBack == null)
                _keyboardDirection = Direction.Forward;
            else if (Input.GetButtonDown(Constants.Input.UseItemForward) &&_tpBack == null)
                _keyboardDirection = Direction.Backward;
        }

        public new void Reload()
        {
            CanUseAbility = true;
            _kartMeshes.SetActive(true);
            _health.UnsetInvincibility();

            if (_tpBack)
            {
                SWExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
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
                    _tpBack.Kart = transform.root;

                    if (Input.GetButton(Constants.Input.UseItemBackward))
                        _throwableLauncher.Throw(throwable, Direction.Forward);
                    else if (Input.GetButton(Constants.Input.UseItemForward))
                        _throwableLauncher.Throw(throwable, Direction.Backward);
                    else
                    {
                        if (_throwingDirection.CurrentDirection == Direction.Default)
                            _throwableLauncher.Throw(throwable, Direction.Backward);
                        else
                            _throwableLauncher.Throw(throwable, _throwingDirection.CurrentDirection);
                    }
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
            yield return new WaitForSeconds(0.25f);
            _kartMeshes.SetActive(true);
            _health.UnsetInvincibility();
            SWExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useTpBackSound);
            if (_tpBack != null)
            {
                var y = _tpBack.transform.position.y + _tPBackSettings.IncreasedYPositionOnTP;
                _rb.transform.position = new Vector3(_tpBack.transform.position.x, y, _tpBack.transform.position.z);
                _rb.transform.rotation = GetKartRotation();
                Destroy(_tpBack.gameObject);
            }
        }
    }
}

     * */
}
