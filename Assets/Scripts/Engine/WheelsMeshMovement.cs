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
            var angleLeft = FrontWheelLeft.transform.localEulerAngles;
            FrontWheelLeft.transform.localEulerAngles = new Vector3(angleLeft.x, currentAngle * 30, 0);
            var angleRight = FrontWheelRight.transform.localEulerAngles;
            FrontWheelRight.transform.localEulerAngles = new Vector3(angleRight.x, currentAngle * 30, 0);
        }

        public void FrontWheelsRotation(float playerVelocity)
        {            
            if (Mathf.Abs(playerVelocity) > 0.01f)
            {
                FrontWheelLeft.transform.Rotate(Vector3.right * playerVelocity);
                FrontWheelRight.transform.Rotate(Vector3.right * playerVelocity);
            }
        }

        public void BackWheelsRotation(float playerVelocity)
        {
            if (Mathf.Abs(playerVelocity) > 0.01f)
            {
                BackWheelsLeft.transform.Rotate(Vector3.right * playerVelocity);
                BackWheelsRight.transform.Rotate(Vector3.right * playerVelocity);
            }
        }
    }
}
