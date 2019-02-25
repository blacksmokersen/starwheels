using UnityEngine;
using UnityEngine.Events;
using Common.PhysicsUtils;
using Bolt;

namespace Steering
{
    public class SteeringWheel : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;

        [SerializeField] private Animator _animatorChar;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool CanSteer = true;
        public enum TurnState { NotTurning, Left, Right }

        [Header("Turn Torques")]
        public SteeringWheelSettings Settings;

        [Header("States")]
        public TurnState TurningState = TurnState.NotTurning;
        public bool InversedDirections = false;

        [Header("Options")]
        [Tooltip("This is an optional field to make turn possible only if grounded.")]
        public GroundCondition _groundCondition;
        [Tooltip("This is an optional field to make turn possible only if a certain speed is reached.")]
        public Engine.EngineBehaviour _engine;

        [Header("Events")]
        public UnityEvent<TurnState> OnTurn;
        public FloatEvent OnTurnValueChanged;

        private Rigidbody _rb;
        private float _turnValue;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled && CanSteer)
            {
                _turnValue = Input.GetAxis(Constants.Input.TurnAxis);
            }
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
            input.Turn = _turnValue;

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
                rb = TurnUsingTorque(cmd.Input.Turn,rb);
                cmd.Result.Velocity = rb.velocity;
            }
        }
        public Rigidbody TurnUsingTorque(float turnValue, Rigidbody rb)
        {
            if (CanTurn())
            {
                SetTurnState(turnValue);
                turnValue = InversedTurnValue(turnValue);

                // TurnSpeed Limiter
                var ActualTurnTorque = Settings.TurnTorque;

                if (Mathf.Abs(turnValue) <= 0.3f)
                    ActualTurnTorque = Settings.TurnTorque/4;
                else if (Mathf.Abs(turnValue) >= 0.3f && Mathf.Abs(turnValue) <= 0.6f)
                    ActualTurnTorque = Settings.TurnTorque/2;
                else if (Mathf.Abs(turnValue) >= 0.6f && Mathf.Abs(turnValue) <= 0.8f)
                    ActualTurnTorque = Settings.TurnTorque / 1.5f;
                else
                    ActualTurnTorque = Settings.TurnTorque;

                OnTurnValueChanged.Invoke(_turnValue);

                if (_groundCondition != null)
                {
                    if (_groundCondition.Grounded)
                    {
                        rb.AddRelativeTorque(Vector3.up * turnValue * ActualTurnTorque, ForceMode.Force);
                    }
                }
                else
                {
                    rb.AddRelativeTorque(Vector3.up * turnValue * ActualTurnTorque, ForceMode.Force);
                    OnTurn.Invoke(TurningState);
                }
            }
            return rb;
        }

        public void InverseDirections()
        {
            InversedDirections = !InversedDirections;
        }

        public void ResetAxisValue()
        {
            _turnValue = 0;
        }
        // PRIVATE

        private void TurnSlowDown(float turnAxis)
        {
            if (TurningState != TurnState.NotTurning)
            {
                float slowdownForce = Settings.SlowdownTurnValue * -Mathf.Abs(turnAxis);
                _rb.AddForce(transform.forward * slowdownForce);
            }
        }

        private void SetTurnState(float turnValue)
        {
            if (turnValue > 0)
            {
                TurningState = TurnState.Right;
                _animatorChar.SetBool("CharNotTurning", false);
                _animatorChar.SetBool("CharTurnLeft", false);
                _animatorChar.SetBool("CharTurnRight", true);
            }
            else if (turnValue < 0)
            {
                TurningState = TurnState.Left;
                _animatorChar.SetBool("CharNotTurning", false);
                _animatorChar.SetBool("CharTurnRight", false);
                _animatorChar.SetBool("CharTurnLeft", true);
            }
            else
            {
                TurningState = TurnState.NotTurning;
                _animatorChar.SetBool("CharNotTurning", true);
                _animatorChar.SetBool("CharTurnRight", false);
                _animatorChar.SetBool("CharTurnLeft", false);
            }
        }

        private bool CanTurn()
        {
            if (_engine.CurrentSpeed > Settings.MinimumSpeedToTurn)
            {
                return true;
            }
            else if (_engine.CurrentSpeed < Settings.MinimumBackSpeedToTurn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private float InversedTurnValue(float value)
        {
            return _engine.CurrentMovingDirection == Engine.MovingDirection.Backward ? -value : value;
        }
    }
}
