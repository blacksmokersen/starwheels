using UnityEngine;
using Kart;

namespace PlayerCamera {
    [RequireComponent(typeof(Camera))]
    public class KartCamera : MonoBehaviour
    {
        private KartStates kartStates;

        public float RotationSpeed;
        public float StabilizeRotationSpeed;
        public float ActualAngle;
        public float MaxTurnAngle;
        public float MaxDriftAngle;
        public GameObject Target;

        private void Awake()
        {
            kartStates = FindObjectOfType<KartStates>();
        }

        private void LateUpdate()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting && kartStates.TurningState != TurningStates.NotTurning)
            {
                IncreaseTurningAngle();
            }
            else if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting)
            {
                IncreaseDriftingAngle();
            }
            else if (kartStates.TurningState == TurningStates.NotTurning && kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                Stabilize();
            }
        }

        private void IncreaseTurningAngle()
        {
            if (Mathf.Abs(ActualAngle) < MaxTurnAngle)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, RotationSpeed);
                    ActualAngle += RotationSpeed;
                }

                if (kartStates.TurningState == TurningStates.Right)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, -RotationSpeed);
                    ActualAngle -= RotationSpeed;
                }
            }
        }

        private void IncreaseDriftingAngle()
        {
            if (Mathf.Abs(ActualAngle) < MaxDriftAngle)
            {
                if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, RotationSpeed);
                    ActualAngle += RotationSpeed;
                }

                if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
                {
                    transform.RotateAround(Target.transform.position, Vector3.up, -RotationSpeed);
                    ActualAngle -= RotationSpeed;
                }
            }
        }

        private void Stabilize()
        {
            if (ActualAngle > 1f)
            {
                transform.RotateAround(Target.transform.position, Vector3.up, -StabilizeRotationSpeed);
                ActualAngle -= StabilizeRotationSpeed;
            }
            else if (ActualAngle < -1f)
            {
                transform.RotateAround(Target.transform.position, Vector3.up, StabilizeRotationSpeed);
                ActualAngle += StabilizeRotationSpeed;
            }
            else
            {
                ActualAngle = 0f;
            }
        }
    }
}
