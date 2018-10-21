using UnityEngine;
using Items;
using CameraUtils;

namespace Controls
{
    public class IonBeamInputs : MonoBehaviour, IControllable
    {
        public static bool IonBeamControlMode;

        private float _horizontalAxis;
        private float _verticalAxis;
        private IonBeamBehaviour _ionBeamBehaviour;
        private IonBeamCamera _ionBeamCamera;

        // CORE

        private void Awake()
        {
            _ionBeamCamera = GameObject.Find("PlayerCamera").GetComponent<IonBeamCamera>();
            _ionBeamBehaviour = GetComponent<IonBeamBehaviour>();
        }

        private void Update()
        {
            MoveCam();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {

            }
        }

        // PRIVATE

        private void MoveCam()
        {
            _horizontalAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);
            _verticalAxis = Input.GetAxis(Constants.Input.TurnAxis);
            _ionBeamCamera.IonBeamCameraControls(_horizontalAxis, _verticalAxis);
        }
    }
}
