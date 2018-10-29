using System.Collections;
using Bolt;
using UnityEngine;
using Common.PhysicsUtils;

namespace Engine
{
    public enum MovingDirection { NotMoving, Forward, Backward }

    public class Engine : EntityBehaviour<IKartState>, IControllable
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

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            CurrentSpeed = transform.InverseTransformDirection(_rb.velocity).z;
            CheckMovingDirection();
            OnVelocityChange.Invoke(_rb.velocity.magnitude);
        }

        // PUBLIC

        public void MapInputs()
        {
            if (_enabled)
            {
                _forwardValue = Input.GetAxis(Constants.Input.Accelerate);
                _backwardValue = Input.GetAxis(Constants.Input.Decelerate);
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
                rb = Accelerate(cmd.Input.Forward,rb);
                rb = Decelerate(cmd.Input.Backward,rb);
                Debug.Log(rb.velocity);
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


        private void Accelerate(float value)
        {
            if (_groundCondition && !_groundCondition.Grounded) return;
            _rb.AddRelativeForce(Vector3.forward * value * Settings.SpeedForce, ForceMode.Force);
        }

        private Rigidbody Accelerate(float value, Rigidbody rb)
        {
            if (_groundCondition && !_groundCondition.Grounded) return rb;
            rb.AddRelativeForce(Vector3.forward * value * Settings.SpeedForce, ForceMode.Force);
            return rb;
        }

        private void Decelerate(float value)
        {
            if (_groundCondition && !_groundCondition.Grounded) return;
            _rb.AddRelativeForce(Vector3.back * value * Settings.SpeedForce / Settings.DecelerationFactor, ForceMode.Force);
        }

        private Rigidbody Decelerate(float value, Rigidbody rb)
        {
            if (_groundCondition && !_groundCondition.Grounded) return rb;
            rb.AddRelativeForce(Vector3.back * value * Settings.SpeedForce / Settings.DecelerationFactor, ForceMode.Force);
            return rb;
        }

        private void CheckMovingDirection()
        {
            if(CurrentSpeed > 0 && CurrentMovingDirection != MovingDirection.Forward)
            {
                CurrentMovingDirection = MovingDirection.Forward;
            }
            else if(CurrentSpeed < 0 && CurrentMovingDirection != MovingDirection.Backward)
            {
                CurrentMovingDirection = MovingDirection.Backward;
            }
            else if(CurrentSpeed == 0 && CurrentMovingDirection != MovingDirection.NotMoving)
            {
                CurrentMovingDirection = MovingDirection.NotMoving;
            }
        }
    }
}
