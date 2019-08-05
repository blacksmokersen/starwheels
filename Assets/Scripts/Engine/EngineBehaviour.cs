using System.Collections;
using Bolt;
using UnityEngine;
using Common.PhysicsUtils;

namespace Engine
{
    public enum MovingDirection { NotMoving, Forward, Backward }

    public class EngineBehaviour : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Forces")]
        public EngineSettings Settings;

        [Header("Events")]
        public FloatEvent OnVelocityChange;

        [Header("States")]
        public MovingDirection CurrentMovingDirection = MovingDirection.NotMoving;

        [Header("Options")]
        [SerializeField] private GroundCondition _groundCondition;

        [Header("BoostMode")]
        [SerializeField] private BoolVariable _boostModeActivated;
        [SerializeField] private FloatVariable _boostModeValue;

        [HideInInspector] public float CurrentSpeed;

        private Rigidbody _rb;

        private float _forwardValue;
        private float _backwardValue;

        private float _curveTime;
        private float _DecCurveTime;
        private float _DecCurveTimeSave;
        private float _baseTimer;
        private float _curveTimeSave;
        private float _startAccCurveTimer;
        private float _startDecCurveTimer;
        private float _curveAccVelocityValue;
        private float _curveDecVelocityValue;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            CurrentSpeed = 0;
        }

        private void Start()
        {
            _startAccCurveTimer = Time.time;
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

            /*

            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f)
            {
                _startAccCurveTimer = Time.time;
                _curveTime = Mathf.Clamp((_startDecCurveTimer - Time.time), 0, Settings.DurationDeccelerationCurve);
                _curveDecVelocityValue = Settings.DeccelerationCurveVelocity.Evaluate(_curveTime);
            }
            else
            {
                _curveTime = Mathf.Clamp((Time.time - _startAccCurveTimer), 0, Settings.DurationAccelerationCurve);
                _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                _startDecCurveTimer = Time.time + Settings.DurationDeccelerationCurve;
            }
            */




            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f && CurrentSpeed < 20)
            {
                if (CurrentSpeed > 3)
                {
                    _curveTimeSave = _curveTime;
                    _DecCurveTime = Mathf.Clamp(((Time.time) - _startDecCurveTimer), 0, Settings.DurationAccelerationCurve);
                }
                else
                {
                    _curveTime = 0;
                    _curveTimeSave = 0;
                    _DecCurveTime = 0;
                    _DecCurveTimeSave = 0;
                }
                _startAccCurveTimer = Time.time;

            }
            else
            {
                _DecCurveTimeSave = _DecCurveTime;
                //  _curveTime = Mathf.Clamp((Time.time - (_startAccCurveTimer - _DecCurveTime)), 0, Settings.DurationAccelerationCurve);
                _curveTime = Mathf.Clamp((Time.time - (_startAccCurveTimer - (_curveTimeSave - _DecCurveTimeSave))), 0, Settings.DurationAccelerationCurve);
                _startDecCurveTimer = Time.time;

            }




            #region OldStuff
            /*
            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f)
            {
                _startAccCurveTimer = Time.time;
                _baseTimer = Mathf.Clamp((Time.time - _startDecCurveTimer), 0, Settings.DurationDeccelerationCurve);
                _curveTimeSave = _curveTime;
                _curveTimeSave = _baseTimer;


            }
            else
            {

                //  _curveTime = Mathf.Clamp((Time.time - _startAccCurveTimer), 0, Settings.DurationAccelerationCurve);

                // _baseTimer = Mathf.Clamp((Time.time - _startAccCurveTimer), 0, Settings.DurationAccelerationCurve);

                _curveTime = Mathf.Clamp((Time.time - _startAccCurveTimer) + _curveTimeSave, 0, Settings.DurationAccelerationCurve);
                _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                _curveDecVelocityValue = Settings.DeccelerationCurveVelocity.Evaluate(Settings.DurationAccelerationCurve -_baseTimer);
                _startDecCurveTimer = Time.time;
            }
            */



            /*
            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f)
            {
                _startAccCurveTimer = Time.time;
                _curveTime = Mathf.Clamp((Time.time - _startDecCurveTimer), 0, Settings.DurationDeccelerationCurve);
                _curveDecVelocityValue = Settings.DeccelerationCurveVelocity.Evaluate(_curveTime);
            }
            else
            {
                _curveTime = Mathf.Clamp((Time.time - _startAccCurveTimer), 0, Settings.DurationAccelerationCurve);
                _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                _startDecCurveTimer = Time.time;
            }
            */






            /*
            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f)
            {
                _startAccCurveTimer = Time.time;
                _curveTime = Time.time + _startDecCurveTimer;
            }
            else
            {
                _curveTime = Mathf.Clamp((Time.time - _startAccCurveTimer), 0, Settings.AccelerationCurveVelocity.length);
                _startDecCurveTimer = Time.time;
            }
            */


            /*
            if (Input.GetAxis(Constants.Input.Accelerate) <= 0.1f && CurrentSpeed <= 5)
                _startCurveTimer = Time.time;
            else if (Input.GetAxis(Constants.Input.Accelerate) <= 0.9f && CurrentSpeed > 10)
            {
                //  _startCurveTimer = Time.time - Settings.CurveVelocity.length / 2;
                _startCurveTimer += Settings.SpeedInertiaLoss;
            }
            else if (Input.GetAxis(Constants.Input.Accelerate) > 0.1f)
            {
                _curveTime = Mathf.Clamp((Time.time - _startCurveTimer), 0, Settings.AccelerationCurveVelocity.length);
            }
            */
            #endregion
        }

        private Rigidbody Accelerate(float value, Rigidbody rb)
        {
            // Debug.Log("CurveTimer = " + _curveTime);
            // Debug.Log("_curveTimeSave = " + _DecCurveTime);
            //  Debug.Log("_curveTimeSave = " + _curveTimeSave);
            //Debug.Log("_deccurveTime = " + _DecCurveTimeSave);
            // Debug.Log("BaseTimer = " + _baseTimer);
            // Debug.Log("ACCCurveValue = " + _curveAccVelocityValue);
            //   Debug.Log("DECCurveValue = " + _curveDecVelocityValue);
            //   Debug.Log("ACCCurveTimer = " + _startAccCurveTimer);
            //   Debug.Log("DECCurveTimer = " + _startDecCurveTimer);


            if (_groundCondition && !_groundCondition.Grounded) return rb;

            _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
            rb.AddRelativeForce(Vector3.forward * value * _curveAccVelocityValue, ForceMode.Force);

            if (_boostModeActivated.Value)
            {
                rb.AddRelativeForce(Vector3.forward * value * _boostModeValue.Value, ForceMode.Force);
            }


            #region OldStuff2
            /*
            if (_curveDecVelocityValue > _curveAccVelocityValue)
            {
                //  Debug.Log("CurveDecValue = " + _curveDecVelocityValue);
                //  _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                rb.AddRelativeForce(Vector3.forward * value * _curveDecVelocityValue, ForceMode.Force);
            }
            else
            {
                //  Debug.Log("CurveAccValue = " + _curveAccVelocityValue);
                // _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                rb.AddRelativeForce(Vector3.forward * value * _curveAccVelocityValue, ForceMode.Force);
            }
            */



            /*
            if (_baseTimer != 1)
            {
               // Debug.Log("CurveDecValue = " + _curveDecVelocityValue);
                rb.AddRelativeForce(Vector3.forward * value * _curveDecVelocityValue, ForceMode.Force);
            }
            else
            {
              //  Debug.Log("CurveAccValue = " + _curveAccVelocityValue);
                rb.AddRelativeForce(Vector3.forward * value * _curveAccVelocityValue, ForceMode.Force);
            }
            */


            /*
            if (_curveTimeSave > _curveTime)
            {
                  Debug.Log("CurveDecValue = " + _curveDecVelocityValue);
                  _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                rb.AddRelativeForce(Vector3.forward * value * _curveDecVelocityValue, ForceMode.Force);
            }
            else
            {
                  Debug.Log("CurveAccValue = " + _curveAccVelocityValue);
                 _curveAccVelocityValue = Settings.AccelerationCurveVelocity.Evaluate(_curveTime);
                rb.AddRelativeForce(Vector3.forward * value * _curveAccVelocityValue, ForceMode.Force);
            }
            */

            #endregion

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
