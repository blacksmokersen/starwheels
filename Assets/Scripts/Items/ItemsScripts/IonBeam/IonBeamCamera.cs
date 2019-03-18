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
        [HideInInspector] public CinemachineTransposer Transposer;
        [HideInInspector] public CinemachineComposer Composer;
      //  [HideInInspector] public CinemachineCollider Collider;

        [Space]
        [Header("This Component Should Stay Inactive")]
        [Space]

        [SerializeField] private float _speedCamMovements;
        [SerializeField] private CinemachineVirtualCamera _playerCameraCinemachine;
      //  [SerializeField] private CameraTurnEffect _cameraTurnEffect;
        [SerializeField] private float _ionBeamCamZExpand;
        [SerializeField] private float _ionBeamCamYExpand;
        [SerializeField] CameraSettings _cameraSettings;
        [SerializeField] private Texture2D _crosshairIonBeam;

        private CinemachineVirtualCamera _cinemachine;
        private CinemachineOrbitalTransposer _orbiter;
        private Animator _animator;


        private Coroutine _cameraIonBeamBehaviour;

       // private string _turnCamInputName = "RightJoystickHorizontal";
        private float _currentTimer;
        private bool _showCrosshair;
        private bool _isCameraOnTop = false;
        private IonBeamBehaviour _ionBeamBehaviour;

        //CORE

        private void Awake()
        {
            _cinemachine = GetComponent<CinemachineVirtualCamera>();
            Transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
            Composer = _cinemachine.GetCinemachineComponent<CinemachineComposer>();
            _orbiter = _cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            _animator = GetComponent<Animator>();

            //  Collider = GetComponent<CinemachineCollider>();
        }

        private void Start()
        {
          //  _orbiter.m_XAxis.m_InputAxisName = _turnCamInputName;
            _cinemachine.Follow = _playerCameraCinemachine.Follow;
            _cinemachine.LookAt = _playerCameraCinemachine.LookAt;
        }

        //BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            if (_ionBeamBehaviour && evnt.VictimEntity.isOwner)
            {
                CameraReset();
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
            Transposer.m_FollowOffset.z += horizontal * _speedCamMovements * Time.deltaTime;
            Transposer.m_FollowOffset.x += vertical * _speedCamMovements * Time.deltaTime;
        }

        public void IonBeamCameraBehaviour(bool direction)
        {
            if (direction)
            {
                StartExpandingCamera();
                /*
                if (_cameraIonBeamBehaviour != null)
                    StopCoroutine(_cameraIonBeamBehaviour);
                _cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamExpand(_ionBeamCamZExpand, _ionBeamCamYExpand, 1f));
                */
            }
            else
            {
                ResetCamera();
                /*
                if (_cameraIonBeamBehaviour != null)
                    StopCoroutine(_cameraIonBeamBehaviour);
                _cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(_cameraSettings.BaseCamPosition.z, _cameraSettings.BaseCamPosition.y, 0.5f));
                */
            }
        }

        public void CameraReset()
        {
            StopAllCoroutines();
            Composer.enabled = true;
            _showCrosshair = false;
            _isCameraOnTop = false;
            Transposer.m_FollowOffset.x = 0;
            Transposer.m_FollowOffset.z = _cameraSettings.BaseCamPosition.z;
            Transposer.m_FollowOffset.y = _cameraSettings.BaseCamPosition.y;
            _playerCameraCinemachine.enabled = true;
          //  _cameraTurnEffect.Enabled = true;
        }

        public bool IsCameraOnTop()
        {
            return _isCameraOnTop;
        }


        public void CameraIsFullyExpanded()
        {
            Composer.enabled = false;
            _showCrosshair = true;
            _isCameraOnTop = true;
        }


        //PRIVATE

        private void StartExpandingCamera()
        {
            _playerCameraCinemachine.enabled = false;
            _animator.SetTrigger("StartExpandCameraTrigger");
            ChangeRenderOnTaGGameobjects(false);
        }

        private void ResetCamera()
        {
            Debug.Log("REsetCam");
            _animator.SetTrigger("ResetCameraTrigger");
            Composer.enabled = true;
            _showCrosshair = false;
            _isCameraOnTop = false;
            ChangeRenderOnTaGGameobjects(true);
            _playerCameraCinemachine.enabled = true;
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




            /*
            if (GameObject.FindGameObjectWithTag("IonBeamCamIgnore") != null)
                GameObject.FindGameObjectWithTag("IonBeamCamIgnore").GetComponent<Renderer>().enabled = state;
                */

        }

        IEnumerator CameraIonBeamExpand(float endValueZ, float endValueY, float expandDuration)
        {
            //  Collider.enabled = false;

            // _cameraTurnEffect.CenterCamera();
            // _cameraTurnEffect.CenterOrbiter();
            // _cameraTurnEffect.Enabled = false;
            _playerCameraCinemachine.enabled = false;
            Composer.enabled = false;

            float startDynamicCamValueZ = Transposer.m_FollowOffset.z;
            float startDynamicCamValueY = Transposer.m_FollowOffset.y;

            var rotation = transform.rotation;

            _currentTimer = 0f;
            while (_currentTimer < expandDuration)
            {
                //transform.rotation.SetEulerAngles.x = Mathf.Lerp(rotation.x, 90, _currentTimer / expandDuration);

                transform.rotation = new Quaternion(transform.rotation.w, Mathf.Lerp(transform.rotation.x, 90, _currentTimer / expandDuration), transform.rotation.y, transform.rotation.z);

                Transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, endValueZ, _currentTimer / expandDuration);
                Transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, endValueY, _currentTimer / expandDuration);
                _currentTimer += Time.deltaTime;
                yield return null;
            }
            // transform.rotation = new Quaternion(Mathf.Lerp(transform.rotation.x, 90, _currentTimer / boostDuration), 0, 0, 0);
            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
            ChangeRenderOnTaGGameobjects(false);
            _showCrosshair = true;
            _isCameraOnTop = true;
        }

        IEnumerator CameraIonBeamReset(float returnValueZ, float returnValueY, float resetDuration)
        {
            _showCrosshair = false;
            _isCameraOnTop = false;
            float startDynamicCamValueX = Transposer.m_FollowOffset.x;
            float startDynamicCamValueZ = Transposer.m_FollowOffset.z;
            float startDynamicCamValueY = Transposer.m_FollowOffset.y;

            _currentTimer = 0f;

            while (_currentTimer < resetDuration)
            {
                Transposer.m_FollowOffset.x = Mathf.Lerp(startDynamicCamValueX, 0, _currentTimer / resetDuration);
                Transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, returnValueZ, _currentTimer / resetDuration);
                Transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, returnValueY, _currentTimer / resetDuration);
                _currentTimer += Time.deltaTime;
                yield return null;
            }

            Transposer.m_FollowOffset.y = returnValueY;

            _playerCameraCinemachine.enabled = true;

            /*
            if (Transposer.m_FollowOffset.y > returnValueY)
            {
                // Security for lack of precision of Time.deltaTime
                _cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(returnValueZ, returnValueY, 0.5f));
            }
            */

            // Collider.enabled = true;
            //  _cameraTurnEffect.Enabled = true;
            //  _cameraTurnEffect.CenterCamera();
            // _cameraTurnEffect.CenterOrbiter();
            ChangeRenderOnTaGGameobjects(true);
        }
    }
}
