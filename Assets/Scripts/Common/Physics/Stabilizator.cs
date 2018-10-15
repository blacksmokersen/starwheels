using UnityEngine;

namespace Common.PhysicsUtils
{
    public class Stabilizator : MonoBehaviour
    {
        public float RotationStabilizationSpeed = .25f;

        private bool _activated = false;

        // CORE

        public void Update()
        {
            if(_activated)
                StabilizeRotation();
        }

        // PUBLIC

        public void Activate()
        {
            _activated = true;
        }

        public void Deactivate()
        {
            _activated = false;
        }

        // PRIVATE

        private void StabilizeRotation()
        {
            var actualRotation = transform.localRotation;
            actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed * Time.deltaTime);
            actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed * Time.deltaTime);
            transform.localRotation = actualRotation;
        }
    }
}
