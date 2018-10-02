using UnityEngine;

namespace Common.PhysicsUtils
{
    public class Stabilizator : MonoBehaviour
    {
        public float RotationStabilizationSpeed = .25f;

        public void StabilizeRotation()
        {
            var actualRotation = transform.localRotation;
            actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed);
            actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed);
            transform.localRotation = actualRotation;
        }
    }
}
