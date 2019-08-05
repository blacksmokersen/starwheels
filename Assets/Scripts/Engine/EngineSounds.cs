using UnityEngine;
using System.Collections;
using System;

namespace Engine
{
    public class EngineSounds : MonoBehaviour
    {
        [Header("Engine")]
        public AudioSource MotorFullSource;

        [SerializeField] private float _minimumEnginePitch;
        [SerializeField] private float _maximumEnginePitch;

        private bool _routineAlreadyStarted = false;

        private float _bufferMinimumEnginePitch;
        private float _bufferMaximumEnginePitch;
        private Coroutine _stepsCoroutine;
        private int _stepNumber = 0;

        //CORE

        private void Awake()
        {
            PlayMotorFullSound();
            _bufferMinimumEnginePitch = _minimumEnginePitch;
            _bufferMaximumEnginePitch = _maximumEnginePitch;
        }

        //PUBLIC

        public void SetMotorFullPitch(float speed)
        {
            var resultingPitch = (_bufferMinimumEnginePitch + _bufferMaximumEnginePitch * speed) / 27;

            if (resultingPitch > 0)
            {
                MotorFullSource.pitch = resultingPitch;
            }
            else
            {
                MotorFullSource.pitch = Mathf.Abs(resultingPitch);
            }

            if (Input.GetButtonDown(Constants.Input.Accelerate))
            {
                if (!_routineAlreadyStarted)
                {
                    StartCoroutine(InvokeMethod(EngineSteps, 1f, 50));
                    _routineAlreadyStarted = true;
                }
            }

            if (Input.GetButtonUp(Constants.Input.Accelerate))
            {
                _routineAlreadyStarted = false;
                EngineStepsReset();
                StopAllCoroutines();
            }
        }

        //PRIVATE

        private void EngineSteps()
        {
            if (_stepsCoroutine == null)
                _stepsCoroutine = StartCoroutine(IncreasePitch());
            else
            {
                StopCoroutine(_stepsCoroutine);
                _stepsCoroutine = StartCoroutine(IncreasePitch());
            }
        }

        private void EngineStepsReset()
        {
            _stepNumber = 0;
            _bufferMinimumEnginePitch = _minimumEnginePitch;
            _bufferMaximumEnginePitch = _maximumEnginePitch;
        }

        private void PlayMotorFullSound()
        {
            MotorFullSource.Play();
        }

        private void StopMotorFullSound()
        {
            MotorFullSource.Stop();
        }

        private void IncreasePitch(float value)
        {
            _bufferMinimumEnginePitch += value;
            _bufferMaximumEnginePitch += value;
        }

        public IEnumerator InvokeMethod(Action method, float interval, int invokeCount)
        {
            for (int i = 0; i < invokeCount; i++)
            {
                _stepNumber++;
                method();
                yield return new WaitForSeconds(interval);
                if (_stepNumber <= 2)
                    interval += 2;
            }
        }

        public IEnumerator IncreasePitch()
        {
            _bufferMinimumEnginePitch = _minimumEnginePitch;
            _bufferMaximumEnginePitch = _maximumEnginePitch;

            if (_stepNumber >= 4)
                IncreasePitch(0.1f);

            var _currentTimer = 0f;
            while (_currentTimer < 10)
            {
                _bufferMinimumEnginePitch += 0.0005f;
                _bufferMaximumEnginePitch += 0.0005f;
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
