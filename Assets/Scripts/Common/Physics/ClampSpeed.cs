﻿using System.Collections;
using UnityEngine;

namespace Common.PhysicsUtils
{
    public class ClampSpeed : MonoBehaviour
    {
        [Header("Base Max Speed")]
        public float ControlMaxSpeed;
        [Header("Current Max Speed(Dont touch that)")]
        [HideInInspector] public float CurrentMaxSpeed;

        private Rigidbody _rigidbody;

        // CORE

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();
            CurrentMaxSpeed = ControlMaxSpeed;
        }

        private void FixedUpdate()
        {
            ClampMagnitude();
        }

        // PUBLIC

        public void SetClampMagnitude(float magnitude)
        {
            CurrentMaxSpeed = magnitude;
        }

        public void ClampForXSeconds(float magnitude, float seconds)
        {
            StartCoroutine(ClampForXSecondsRoutine(magnitude, seconds));
        }

        public void ResetClampMagnitude()
        {
            CurrentMaxSpeed = ControlMaxSpeed;
        }

        // PRIVATE

        private void ClampMagnitude()
        {
            if (CurrentMaxSpeed > 0)
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, CurrentMaxSpeed);
            }
        }

        private IEnumerator ClampForXSecondsRoutine(float magnitude, float seconds)
        {
            CurrentMaxSpeed = magnitude;
            yield return new WaitForSeconds(seconds);
            CurrentMaxSpeed = ControlMaxSpeed;
        }
    }
}
