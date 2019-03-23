using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Bolt;
using CameraUtils;
using Items;

namespace CameraUtils
{
    public class IonBeamCamera : CameraTarget
    {
        [Space]
        [Header("This Component Should Stay Inactive")]
        [Space]

        [SerializeField] private float _speedCamMovements;
        [SerializeField] private GameObject _playerCamera;
        [SerializeField] CameraSettings _cameraSettings;
        [SerializeField] private Texture2D _crosshairIonBeam;

        private Animator _animator;
        private bool _showCrosshair;
        private bool _isCameraOnTop = false;
        private IonBeamBehaviour _ionBeamBehaviour;

        //CORE

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        //BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            if (_ionBeamBehaviour && evnt.VictimEntity.isOwner)
            {
                ResetCamera();
                _ionBeamBehaviour.DisableIonBeam();
            }
        }

        //PUBLIC

        public void GetIonBeamBehaviour(IonBeamBehaviour ionBeamBehaviour)
        {
            _ionBeamBehaviour = ionBeamBehaviour;
        }

        public void IonBeamCameraControls(float horizontal, float vertical)
        {
            transform.position += transform.forward * horizontal * _speedCamMovements * Time.deltaTime;
            transform.position += transform.right * vertical * _speedCamMovements * Time.deltaTime;
        }

        public void IonBeamCameraBehaviour(bool direction)
        {
            if (direction)
                StartExpandingCamera();
            else
                ResetCamera();
        }

        public bool IsCameraOnTop()
        {
            return _isCameraOnTop;
        }

        public void CameraIsFullyExpanded()
        {
            _showCrosshair = true;
            _isCameraOnTop = true;
        }

        public void ResetCameraTransform()
        {
            transform.position = _playerCamera.transform.position;
            transform.rotation = new Quaternion(0,
                _playerCamera.transform.rotation.y,
                _playerCamera.transform.rotation.z,
                _playerCamera.transform.rotation.w);
        }

        //PRIVATE

        private void StartExpandingCamera()
        {
            _animator.SetTrigger("StartExpandCameraTrigger");
            ChangeRenderOnTaGGameobjects(false);
        }

        private void ResetCamera()
        {
            _animator.SetTrigger("ResetCameraTrigger");
            _showCrosshair = false;
            _isCameraOnTop = false;
            ChangeRenderOnTaGGameobjects(true);
        }

        private void OnGUI()
        {
            if (_showCrosshair)
            {
                float xMin = (Screen.width / 2) - (_crosshairIonBeam.width / 2);
                float yMin = (Screen.height / 2) - (_crosshairIonBeam.height / 2);
                GUI.DrawTexture(new Rect(xMin, yMin, _crosshairIonBeam.width, _crosshairIonBeam.height), _crosshairIonBeam);
            }
        }

        private void ChangeRenderOnTaGGameobjects(bool state)
        {
            GameObject[] TaggedGameobjectsToIgnore;
            TaggedGameobjectsToIgnore = GameObject.FindGameObjectsWithTag(Constants.Tag.IonBeamCamIgnore);

            foreach (GameObject GoToIgnore in TaggedGameobjectsToIgnore)
            {
                GoToIgnore.GetComponent<Renderer>().enabled = state;
            }
        }
    }
}
