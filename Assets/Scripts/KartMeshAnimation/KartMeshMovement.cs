using UnityEngine;

namespace Animations
{
    public class KartMeshMovement : BaseKartComponent
    {
        [Header("Meshes")]
        public GameObject frontWheelLeft;
        public GameObject frontWheelRight;
        public GameObject backWheelsL;
        public GameObject backWheelsR;

        private float MaxMeshAngleDrift;
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
            frontWheelLeft.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
            frontWheelRight.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
        }

        public void FrontWheelsRotation(float playerVelocity)
        {
            wheelsSpeed = -playerVelocity;
            if (playerVelocity > 0.01f)
            {
                frontWheelLeft.transform.Rotate(Vector3.right * wheelsSpeed);
                frontWheelRight.transform.Rotate(Vector3.right * wheelsSpeed);
            }
        }

        public void BackWheelsRotation(float playerVelocity)
        {
            wheelsSpeed = playerVelocity;
            if (playerVelocity > 0.01f)
            {
                backWheelsL.transform.Rotate(Vector3.right * wheelsSpeed);
                backWheelsR.transform.Rotate(Vector3.right * wheelsSpeed);
            }
        }
    }
}
