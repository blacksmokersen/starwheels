using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Common.PhysicsUtils;

namespace Abilities.Jump
{
    public class JumpingAbility : Ability, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Events")]
        public UnityEvent OnFirstJump;
        public DirectionEvent OnSecondJump;
        public UnityEvent OnJumpReload;

        [Header("Conditions")]
        [SerializeField] private GroundCondition _groundCondition;

        private JumpSettings _jumpSettings;
        private Rigidbody _rb;
        private bool _hasDoneFirstJump = false;
        private bool _straightUpSecondJump = false;
        private Coroutine _timeBetweenFirstAndSecondJump;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            _jumpSettings = (JumpSettings) abilitySettings;
            _groundCondition.OnHitGround.AddListener(() => { _hasDoneFirstJump = false; });
        }

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public new void Reload()
        {
            CanUseAbility = true;
        }

            public void FirstJump()
        {
            _rb.AddRelativeForce(Vector3.up * _jumpSettings.FirstJumpForce, ForceMode.Impulse);
            _hasDoneFirstJump = true;
            OnFirstJump.Invoke();
        }

        public void SecondJump(JoystickValues joystickValues)
        {
            _hasDoneFirstJump = false;
            _straightUpSecondJump = false;

            var direction = Direction.Default;

            Vector3 forceDirection;
            if (joystickValues.X < -0.5f)
            {
                forceDirection = Vector3.left;
                direction = Direction.Left;
            }
            else if (joystickValues.X > 0.5f)
            {
                forceDirection = Vector3.right;
                direction = Direction.Right;
            }
            else if (joystickValues.Y > 0.5f)
            {
                forceDirection = Vector3.forward;
                direction = Direction.Forward;
            }
            else if (joystickValues.Y < -0.5f)
            {
                forceDirection = Vector3.back;
                direction = Direction.Backward;
            }
            else
            {
                forceDirection = Vector3.forward;
                direction = Direction.Forward;

                //  forceDirection = Vector3.up;
                //  _straightUpSecondJump = true;
            }

            var forceUp = Vector3.up * _jumpSettings.SecondJumpUpForce;
            var forceDirectional = forceDirection * _jumpSettings.SecondJumpLateralForces;
            if (_straightUpSecondJump)
                _rb.AddRelativeForce(forceUp, ForceMode.Force);
            else
            {
                _rb.AddRelativeForce(forceUp + forceDirectional, ForceMode.Force);
                StartCoroutine(CancelJumpVelocity(forceDirectional));
            }
            OnSecondJump.Invoke(direction);
        }

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
            {
                JoystickValues joystickValues = new JoystickValues()
                {
                    X = Input.GetAxis(Constants.Input.TurnAxis),
                    Y = Input.GetAxis(Constants.Input.UpAndDownAxis)
                };
                UseAbility(joystickValues);
            }
        }

        // PRIVATE

        private void UseAbility(JoystickValues joystickValues)
        {
            if (CanUseAbility)
            {
                if (!_hasDoneFirstJump && _groundCondition.Grounded)
                {
                    FirstJump();
                    _timeBetweenFirstAndSecondJump = StartCoroutine(TimeBetweenFirstAndSecondJump());
                }
                else if (_hasDoneFirstJump && !_groundCondition.Grounded)
                {
                    SecondJump(joystickValues);
                    if (_timeBetweenFirstAndSecondJump != null)
                    {
                        StopCoroutine(_timeBetweenFirstAndSecondJump);
                    }
                    StartCoroutine(Cooldown());
                    _hasDoneFirstJump = false;
                }
            }
        }

        private IEnumerator TimeBetweenFirstAndSecondJump()
        {
            yield return new WaitForSeconds(_jumpSettings.MaxTimeBetweenFirstAndSecondJump);
            StartCoroutine(Cooldown());
        }

        private IEnumerator CancelJumpVelocity(Vector3 velocityToCancel)
        {
            yield return new WaitForSeconds(_jumpSettings.TimeBeforeCancelVelocity);
            var locVel = transform.InverseTransformDirection(_rb.velocity);
            var _currentTimer = 0f;
            while (_currentTimer < _jumpSettings.CancelSmootherDuration)
            {
                locVel = transform.InverseTransformDirection(_rb.velocity);
                _rb.velocity -= transform.TransformDirection(new Vector3(locVel.x/10,0,0));
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            locVel.x = 0;
            _rb.velocity = transform.TransformDirection(locVel);

        }
    }
}
