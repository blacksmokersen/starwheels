using UnityEngine;

namespace Kart
{
    /*
     * Class for handling the Kart orientation (drift, turn etc.)
     */ 
    public class KartOrientation : MonoBehaviour
    {
        private KartStates kartStates;

        [Header("Turn")]
        public float TurningSpeed;

        [Header("Drift")]
        public float DriftingTurningSpeed;
        [Range(0, 1)] public float DriftMaxAngle;
        [Range(0, 1)] public float DriftMinAngle;

        public float RotationStabilizationSpeed;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
        }

        private void FixedUpdate()
        {
            StabilizeRotation();
        }

        public void Turn(float value)
        {
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                transform.Rotate(new Vector3(0, value * TurningSpeed * Time.deltaTime, 0));
            }
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight || kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, value * DriftingTurningSpeed * Time.deltaTime, 0));
            }
        }

        public void DriftTurn(float angle)
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, Mathf.Clamp(angle, -DriftMaxAngle, -DriftMinAngle) * DriftingTurningSpeed * Time.deltaTime, 0));
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {                
                transform.Rotate(new Vector3(0, Mathf.Clamp(angle, DriftMinAngle, DriftMaxAngle) * DriftingTurningSpeed * Time.deltaTime, 0));
            }
        }
        
        public void StabilizeRotation()
        {
            if(kartStates.AirState == AirStates.InAir)
            {
                var actualRotation = transform.localRotation;
                actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed);
                actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed);
                transform.localRotation = actualRotation;
            }
        }
    }
}