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
        private bool _resetFrontWheelsPosition = false;

        // PUBLIC

        public void FrontWheelsTurn(float currentAngle)
        {

            if (Mathf.Abs(Input.GetAxis(Constants.Input.TurnAxis)) > 0.05f)
            {
                var angleLeft = FrontWheelLeft.transform.localEulerAngles;
                FrontWheelLeft.transform.localEulerAngles = new Vector3(angleLeft.x, currentAngle * 30, 0);
                var angleRight = FrontWheelRight.transform.localEulerAngles;
                FrontWheelRight.transform.localEulerAngles = new Vector3(angleRight.x, currentAngle * 30, 0);
                _resetFrontWheelsPosition = true;
            }
            else
            {
                if (_resetFrontWheelsPosition)
                {
                    var angleLeft = FrontWheelLeft.transform.localEulerAngles;
                    FrontWheelLeft.transform.localEulerAngles = new Vector3(angleLeft.x, 0, 0);
                    var angleRight = FrontWheelRight.transform.localEulerAngles;
                    FrontWheelRight.transform.localEulerAngles = new Vector3(angleRight.x, 0, 0);
                    _resetFrontWheelsPosition = false;
                }
            }
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
