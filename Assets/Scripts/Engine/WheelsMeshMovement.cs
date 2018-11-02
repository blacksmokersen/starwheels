using UnityEngine;

namespace Animations
{
    public class WheelsMeshMovement : MonoBehaviour
    {
        [Header("Meshes")]
        public GameObject FrontWheelLeft;
        public GameObject FrontWheelRight;
        public GameObject BackWheelsLeft;
        public GameObject BackWheelsRight;

        private float _wheelsSpeed;

        // PUBLIC

        public void FrontWheelsTurn(float currentAngle)
        {
            FrontWheelLeft.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
            FrontWheelRight.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
        }

        public void FrontWheelsRotation(float playerVelocity)
        {
            _wheelsSpeed = -playerVelocity;
            if (playerVelocity > 0.01f)
            {
                FrontWheelLeft.transform.Rotate(Vector3.right * _wheelsSpeed);
                FrontWheelRight.transform.Rotate(Vector3.right * _wheelsSpeed);
            }
        }

        public void BackWheelsRotation(float playerVelocity)
        {
            _wheelsSpeed = playerVelocity;
            if (playerVelocity > 0.01f)
            {
                BackWheelsLeft.transform.Rotate(Vector3.right * _wheelsSpeed);
                BackWheelsRight.transform.Rotate(Vector3.right * _wheelsSpeed);
            }
        }
    }
}
