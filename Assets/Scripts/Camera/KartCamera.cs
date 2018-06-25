using UnityEngine;
using Kart;

namespace PlayerCamera {
    [RequireComponent(typeof(Camera))]
    public class KartCamera : MonoBehaviour
    {
        private KartStates kartStates;

        public float RotationSpeed;
        public float ActualAngle;
        public float MaxAngle;
        public GameObject Target;

        private void Awake()
        {
            kartStates = FindObjectOfType<KartStates>();
        }

        private void LateUpdate()
        {
            IncreaseAngle();
            Stabilize();
        }

        private void IncreaseAngle()
        {
            if (Mathf.Abs(ActualAngle) < MaxAngle)
            {
                if (kartStates.TurningState == KartStates.TurningStates.Left)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, RotationSpeed);
                    ActualAngle += RotationSpeed;
                }

                if (kartStates.TurningState == KartStates.TurningStates.Right)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, -RotationSpeed);
                    ActualAngle -= RotationSpeed;
                }
            }
        }

        private void Stabilize()
        {
            if(kartStates.TurningState == KartStates.TurningStates.NotTurning)
            {
                if (ActualAngle > 0.5f)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, -RotationSpeed);
                    ActualAngle -= RotationSpeed;
                }
                else if (ActualAngle < 0.5f)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, RotationSpeed);
                    ActualAngle += RotationSpeed;
                }
            }
        }
    }
}
