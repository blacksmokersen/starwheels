using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Common.PhysicsUtils;

namespace Abilities.Jump
{
    public class JumpingAbility : AbilitiesBehaviour, IControllable
    {
        [Header("Events")]
        public UnityEvent OnFirstJump;
        public DirectionEvent OnSecondJump;
        public UnityEvent OnJumpReload;

        [Header("Forces")]
        [SerializeField] private JumpSettings jumpSettings;

        [SerializeField] private GroundCondition groundCondition;

        [Header("Effects")]
        [SerializeField] private ParticleSystem reloadParticlePrefab;
        [SerializeField] private int reloadParticleNumber;

        private Rigidbody _rb;
        private bool _canUseAbility = true;
        private bool _hasDoneFirstJump = false;
        private bool _straightUpSecondJump = false;
        private Coroutine _timeBetweenFirstAndSecondJump;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        // BOLT

        public override void SimulateController()
        {
            if (abilitiesBehaviourSettings.ActiveAbility == "Jump")
                MapInputs();
        }

        // PUBLIC

        public void FirstJump()
        {
            _rb.AddRelativeForce(Vector3.up * jumpSettings.FirstJumpForce, ForceMode.Impulse);
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
                forceDirection = Vector3.up;
                _straightUpSecondJump = true;
            }

            var forceUp = Vector3.up * jumpSettings.SecondJumpUpForce;
            var forceDirectional = forceDirection * jumpSettings.SecondJumpLateralForces;
            if (_straightUpSecondJump)
                _rb.AddRelativeForce(forceUp, ForceMode.Impulse);
            else
                _rb.AddRelativeForce(forceUp + forceDirectional, ForceMode.Impulse);
            OnSecondJump.Invoke(direction);
        }

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
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
            if (_canUseAbility)
            {
                if (!_hasDoneFirstJump && groundCondition.Grounded)
                {
                    FirstJump();
                    _timeBetweenFirstAndSecondJump = StartCoroutine(TimeBetweenFirstAndSecondJump());
                }
                else if (!groundCondition.Grounded)
                {
                    SecondJump(joystickValues);
                    if (_timeBetweenFirstAndSecondJump != null) { }
                    StopCoroutine(_timeBetweenFirstAndSecondJump);
                    StartCoroutine(Cooldown());
                }
            }
        }

        private IEnumerator TimeBetweenFirstAndSecondJump()
        {
            yield return new WaitForSeconds(jumpSettings.MaxTimeBetweenFirstAndSecondJump);
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            _canUseAbility = false;
            yield return new WaitForSeconds(jumpSettings.CooldownDuration);
            _canUseAbility = true;
            _hasDoneFirstJump = false;
            reloadParticlePrefab.Emit(reloadParticleNumber);
          //  OnJumpReload.Invoke();
        }
    }
}
