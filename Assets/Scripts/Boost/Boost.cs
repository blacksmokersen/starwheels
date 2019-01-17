using System.Collections;
using UnityEngine;
using Common.PhysicsUtils;
using Engine;

namespace Boost
{
    public class Boost : MonoBehaviour
    {
        [Header("Boost Settings")]
        [SerializeField] private BoostSettings _settings;

        [Header("Boost Dependencies")]
        [SerializeField] private ClampSpeedSettings _clampSpeedSettings;
        [SerializeField] private Rigidbody _rb;

        [Header("KartSpecific Boost Dependencies")]
        [SerializeField] private EngineBehaviour _engine;
        [SerializeField] private BoolVariable _boostModeActivated;
        [SerializeField] private FloatVariable _boostModeValue;

        private Coroutine _physicsBoostCoroutine;
        private Coroutine _directImpulseXBoost;
        private Coroutine _uniqueImpulseXBoostWithXMaxClampSpeed;
        private Coroutine _constantXBoostForXSeconds;
        private Coroutine _xBoostOnAccelerationForXSecondsCoroutine;
        private Coroutine _returnToClampSpeedBaseValue;
        private float _controlClampSpeed;
        private float _currentClampSpeed;
        private float _currentTimer;

        // PUBLIC

        public void StartTurbo()
        {
            _controlClampSpeed = _clampSpeedSettings.BaseMaxSpeed;
            StartCoroutine(EnterTurbo());
        }

        private void Update()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {
                //  UniqueImpulseXBoostWithXMaxClampSpeed(5000f, 5, 1, 1);
                XBoostOnAccelerationForXSeconds(500,3,10,1);
            }
        }

        public void UniqueImpulseXBoostWithXMaxClampSpeed(float boostPower, float maxClampSpeed, float maxClampSpeedDuration, float returnToBaseValueDuration)
        {
            _controlClampSpeed = _clampSpeedSettings.BaseMaxSpeed;

            if (_uniqueImpulseXBoostWithXMaxClampSpeed != null)
                StopCoroutine(_uniqueImpulseXBoostWithXMaxClampSpeed);
            if (_returnToClampSpeedBaseValue != null)
                StopCoroutine(_returnToClampSpeedBaseValue);

            _uniqueImpulseXBoostWithXMaxClampSpeed = StartCoroutine(UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(boostPower, maxClampSpeed, maxClampSpeedDuration, returnToBaseValueDuration));
        }


        public void ConstantXBoostForXSeconds(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _controlClampSpeed = _clampSpeedSettings.BaseMaxSpeed;

            if (_constantXBoostForXSeconds != null)
                StopCoroutine(_constantXBoostForXSeconds);
            if (_returnToClampSpeedBaseValue != null)
                StopCoroutine(_returnToClampSpeedBaseValue);

            _constantXBoostForXSeconds = StartCoroutine(ConstantXBoostForXSecondsCoroutine(boostPower, duration, maxClampSpeed, returnToBaseValueDuration));
        }

        public void XBoostOnAccelerationForXSeconds(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _controlClampSpeed = _clampSpeedSettings.BaseMaxSpeed;

            if (_constantXBoostForXSeconds != null)
                StopCoroutine(_constantXBoostForXSeconds);
            if (_returnToClampSpeedBaseValue != null)
                StopCoroutine(_returnToClampSpeedBaseValue);


            _xBoostOnAccelerationForXSecondsCoroutine = StartCoroutine(XBoostOnAccelerationForXSecondsCoroutine(boostPower, duration, maxClampSpeed, returnToBaseValueDuration));
        }

        public void CustomBoostFromBoostSettings(BoostSettings settings)
        {

        }

        // PRIVATE

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(PhysicsBoost(_settings.BoostDuration, _settings.IncreaseMaxSpeedBy));

            yield return new WaitForSeconds(_settings.BoostDuration);
        }

        private IEnumerator PhysicsBoost(float boostDuration, float magnitudeBoost)
        {
            _clampSpeedSettings.CurrentMaxSpeed = Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + magnitudeBoost;

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _rb.AddRelativeForce(Vector3.forward * _settings.BoostPowerImpulse, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _clampSpeedSettings.CurrentMaxSpeed = Mathf.Lerp(_controlClampSpeed + magnitudeBoost, _clampSpeedSettings.CurrentMaxSpeed, _currentTimer / boostDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }






        private IEnumerator UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(float boostPower, float maxClampSpeed, float maxClampSpeedDuration , float returnToBaseValueDuration)
        {
            _clampSpeedSettings.CurrentMaxSpeed += Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + maxClampSpeed;

            _currentTimer = 0f;
            while (_currentTimer < 0.5f)
            {
                _rb.AddRelativeForce(Vector3.forward * boostPower, ForceMode.Force);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }


            _rb.AddRelativeForce(Vector3.forward * boostPower);
            yield return new WaitForSeconds(maxClampSpeedDuration);
            _returnToClampSpeedBaseValue = StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator ConstantXBoostForXSecondsCoroutine(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _clampSpeedSettings.CurrentMaxSpeed += Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + maxClampSpeed;

            _currentTimer = 0f;
            while (_currentTimer < duration)
            {
                _rb.AddRelativeForce(Vector3.forward * boostPower, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _returnToClampSpeedBaseValue = StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator XBoostOnAccelerationForXSecondsCoroutine(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _clampSpeedSettings.CurrentMaxSpeed += Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + maxClampSpeed;
            _boostModeActivated.Value = true;
            _boostModeValue.Value = boostPower;
            yield return new WaitForSeconds(duration);
            _boostModeActivated.Value = false;
            _boostModeValue.Value = 0;

            _returnToClampSpeedBaseValue = StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }


        private IEnumerator ReturnToClampSpeedBaseValue(float clampSpeedValueToDecrease, float returnDuration)
        {
            _currentTimer = 0f;
            while (_currentTimer < returnDuration)
            {
                _clampSpeedSettings.CurrentMaxSpeed = Mathf.Lerp(_controlClampSpeed + clampSpeedValueToDecrease, _clampSpeedSettings.CurrentMaxSpeed, _currentTimer / returnDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
