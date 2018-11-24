using System.Collections;
using Bolt;
using UnityEngine;
using Common.PhysicsUtils;

namespace Engine
{
    public enum MovingDirection { NotMoving, Forward, Backward }

    public class EngineBehaviour : EntityBehaviour<IKartState>, IControllable
    {
        [Header("Forces")]
        public EngineSettings Settings;

        [Header("Events")]
        public FloatEvent OnVelocityChange;

        [Header("States")]
        public MovingDirection CurrentMovingDirection = MovingDirection.NotMoving;

        [Header("Options")]
        [SerializeField] private GroundCondition _groundCondition;

        [HideInInspector] public float CurrentSpeed;

        private Rigidbody _rb;

        private float _forwardValue;
        private float _backwardValue;
        private bool _enabled = true;

        private float _curveTime;
        private float _startCurveTimer;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            CurrentSpeed = 0;
        }

        private void Start()
        {
            _startCurveTimer = Time.time;
        }

        private void FixedUpdate()
        {
            CurrentSpeed = transform.InverseTransformDirection(_rb.velocity).z;
            CheckMovingDirection();
            OnVelocityChange.Invoke(CurrentSpeed);
        }

        // PUBLIC

        public void MapInputs()
        {
            if (_enabled)
            {
                _forwardValue = Input.GetAxis(Constants.Input.Accelerate);
                _backwardValue = Input.GetAxis(Constants.Input.Decelerate);
                CurveVelocityHandler();
            }
            else
            {
                _forwardValue = 0f;
                _backwardValue = 0f;
            }
        }

        public void DisableForXSeconds(float x)
        {
            StartCoroutine(DisableForXSecondsRoutine(x));
        }

        public override void Attached()
        {
            if (!entity.isControlled && entity.isOwner)
            {
                entity.TakeControl();
            }
        }

        public override void SimulateController()
        {
            MapInputs();

            IKartCommandInput input = KartCommand.Create();
            input.Forward = _forwardValue;
            input.Backward = _backwardValue;

            entity.QueueInput(input);
        }

        public override void ExecuteCommand(Command command, bool resetState)
        {
            KartCommand cmd = (KartCommand)command;

            if (resetState)
            {
                Debug.LogWarning("Applying Engine Correction");
            }
            else
            {
                var rb = _rb;
                rb = Accelerate(cmd.Input.Forward, rb);
                rb = Decelerate(cmd.Input.Backward, rb);
                cmd.Result.Velocity = rb.velocity;
            }
        }

        // PRIVATE

        private IEnumerator DisableForXSecondsRoutine(float x)
        {
            _enabled = false;
            yield return new WaitForSeconds(x);
            _enabled = true;
        }

        private void CurveVelocityHandler()
        {
            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f && CurrentSpeed < 25)
                _startCurveTimer = Time.time;
            else if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f && CurrentSpeed > 25)
                _startCurveTimer = Time.time - Settings.CurveVelocity.length / 2;
            else if (Input.GetAxis(Constants.Input.Accelerate) > 0.1f)
                _curveTime = Time.time - _startCurveTimer;
        }

        private Rigidbody Accelerate(float value, Rigidbody rb)
        {
            var curveVelocityValue = Settings.CurveVelocity.Evaluate(_curveTime);
            // Debug.Log("CurveTimer = " + _curveTime);
            // Debug.Log("CurveValue = "+curveVelocityValue);

            if (_groundCondition && !_groundCondition.Grounded) return rb;
            rb.AddRelativeForce(Vector3.forward * value * curveVelocityValue, ForceMode.Force);
            return rb;
        }

        private Rigidbody Decelerate(float value, Rigidbody rb)
        {
            if (_groundCondition && !_groundCondition.Grounded) return rb;
            rb.AddRelativeForce(Vector3.back * value * Settings.BackSpeedForce / Settings.DecelerationFactor, ForceMode.Force);
            return rb;
        }

        private void CheckMovingDirection()
        {
            if (CurrentSpeed > 0 && CurrentMovingDirection != MovingDirection.Forward)
            {
                CurrentMovingDirection = MovingDirection.Forward;
            }
            else if (CurrentSpeed < 0 && CurrentMovingDirection != MovingDirection.Backward)
            {
                CurrentMovingDirection = MovingDirection.Backward;
            }
            else if (CurrentSpeed == 0 && CurrentMovingDirection != MovingDirection.NotMoving)
            {
                CurrentMovingDirection = MovingDirection.NotMoving;
            }
        }
    }
}
