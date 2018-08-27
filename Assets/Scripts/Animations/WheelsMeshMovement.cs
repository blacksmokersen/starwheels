using UnityEngine;

namespace Animations
{
    /*
     * Class used for the rotations of the kart wheels
     * 
     */ 
    public class WheelsMeshMovement : BaseKartComponent
    {
        [Header("Meshes")]
        public GameObject FrontWheelLeft;
        public GameObject FrontWheelRight;
        public GameObject BackWheelsLeft;
        public GameObject BackWheelsRight;

        private float wheelsSpeed;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnVelocityChange += FrontWheelsRotation;
            kartEvents.OnVelocityChange += BackWheelsRotation;
            kartEvents.OnTurn += FrontWheelsTurn;            
        }

        private void FrontWheelsTurn(float currentAngle)
        {
            FrontWheelLeft.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
            FrontWheelRight.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
        }

        private void FrontWheelsRotation(Vector3 playerVelocity)
        {
            wheelsSpeed = -playerVelocity.magnitude;
            if (playerVelocity.magnitude > 0.01f)
            {
                FrontWheelLeft.transform.Rotate(Vector3.right * wheelsSpeed);
                FrontWheelRight.transform.Rotate(Vector3.right * wheelsSpeed);
            }
        }

        private void BackWheelsRotation(Vector3 playerVelocity)
        {
            wheelsSpeed = playerVelocity.magnitude;
            if (playerVelocity.magnitude > 0.01f)
            {
                BackWheelsLeft.transform.Rotate(Vector3.right * wheelsSpeed);
                BackWheelsRight.transform.Rotate(Vector3.right * wheelsSpeed);
            }
        }
    }
}
