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
        [SerializeField] private ClampSpeed _clampSpeed;
        [SerializeField] private Rigidbody _rb;

        [Header("KartSpecific Boost Dependencies")]
        [SerializeField] private EngineBehaviour _engine;

        private Coroutine _physicsBoostCoroutine;
        private Coroutine _directImpulseXBoost;
        private Coroutine _uniqueImpulseXBoostWithXMaxClampSpeed;
        private Coroutine _constantXBoostForXSeconds;
        private Coroutine _xBoostOnAccelerationForXSeconds;
        private Coroutine _returnToClampSpeedBaseValue;
        private float _controlMagnitude;
        private float _currentTimer;

        // PUBLIC

        public void StartTurbo()
        {

            /*
            _controlMagnitude = _clampSpeed.ControlMaxSpeed;
            StartCoroutine(EnterTurbo());
            */

            // ConstantXBoostForXSeconds(0.5f, 3, 40, 1);
            UniqueImpulseXBoostWithXMaxClampSpeed(100000f, 50, 3, 1);
        }

        public void UniqueImpulseXBoost(float boostPower, ForceMode forceMode)
        {
            _rb.AddRelativeForce(Vector3.forward * boostPower, forceMode);
        }

        public void UniqueImpulseXBoostWithXMaxClampSpeed(float boostPower, float maxClampSpeed, float maxClampSpeedDuration, float returnToBaseValueDuration)
        {
            _controlMagnitude = _clampSpeed.ControlMaxSpeed;

            if (_uniqueImpulseXBoostWithXMaxClampSpeed != null)
                StopCoroutine(_uniqueImpulseXBoostWithXMaxClampSpeed);
            if (_returnToClampSpeedBaseValue != null)
                StopCoroutine(_returnToClampSpeedBaseValue);

            _uniqueImpulseXBoostWithXMaxClampSpeed = StartCoroutine(UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(boostPower, maxClampSpeed, maxClampSpeedDuration, returnToBaseValueDuration));
        }


        public void ConstantXBoostForXSeconds(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _controlMagnitude = _clampSpeed.ControlMaxSpeed;

            if (_constantXBoostForXSeconds != null)
                StopCoroutine(_constantXBoostForXSeconds);
            if (_returnToClampSpeedBaseValue != null)
                StopCoroutine(_returnToClampSpeedBaseValue);

            _constantXBoostForXSeconds = StartCoroutine(ConstantXBoostForXSecondsCoroutine(boostPower, duration, maxClampSpeed, returnToBaseValueDuration));
        }

        public void XBoostOnAccelerationForXSeconds(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {





            StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
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
            _clampSpeed.CurrentMaxSpeed = Mathf.Clamp(_clampSpeed.CurrentMaxSpeed, 0, _controlMagnitude) + magnitudeBoost;

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
                _clampSpeed.CurrentMaxSpeed = Mathf.Lerp(_controlMagnitude + magnitudeBoost, _clampSpeed.ControlMaxSpeed, _currentTimer / boostDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }






        private IEnumerator UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(float boostPower, float maxClampSpeed, float maxClampSpeedDuration , float returnToBaseValueDuration)
        {
            _clampSpeed.CurrentMaxSpeed = Mathf.Clamp(_clampSpeed.CurrentMaxSpeed, 0, _controlMagnitude) + maxClampSpeed;
            _rb.AddRelativeForce(Vector3.forward * boostPower);
            yield return new WaitForSeconds(maxClampSpeedDuration);
            _returnToClampSpeedBaseValue = StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator ConstantXBoostForXSecondsCoroutine(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _clampSpeed.CurrentMaxSpeed = Mathf.Clamp(_clampSpeed.CurrentMaxSpeed, 0, _controlMagnitude) + maxClampSpeed;

            _currentTimer = 0f;
            while (_currentTimer < duration)
            {
                _rb.AddRelativeForce(Vector3.forward * boostPower, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _returnToClampSpeedBaseValue = StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator ReturnToClampSpeedBaseValue(float clampSpeedValueToDecrease, float returnDuration)
        {
            _currentTimer = 0f;
            while (_currentTimer < returnDuration)
            {
                _clampSpeed.CurrentMaxSpeed = Mathf.Lerp(_controlMagnitude + clampSpeedValueToDecrease, _clampSpeed.ControlMaxSpeed, _currentTimer / returnDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
