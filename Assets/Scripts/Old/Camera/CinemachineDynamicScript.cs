using System.Collections;
using UnityEngine;
using Cinemachine;
using Controls;
using Kart;

namespace CameraUtils
{
    public class CinemachineDynamicScript : MonoBehaviour
    {
        [Range(8.5f, 15)] public float MaxDistanceCamInBoost;
        public CinemachineTransposer transposer;

        [SerializeField] private float autoCenterTiming;

        private CinemachineVirtualCamera cinemachine;
        private Coroutine cameraBoostCoroutine;
        private CinemachineComposer composer;
        private CinemachineOrbitalTransposer orbiter;
        private bool backCamActivated = false;
        private float currentTimer;
        private KartEngine _kartEngine;
        private KartEvents _kartEvents;

        private void Awake()
        {
            cinemachine = GetComponent<CinemachineVirtualCamera>();
            orbiter = cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            transposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
            composer = cinemachine.GetCinemachineComponent<CinemachineComposer>();
        }

        private void Update()
        {
            SpeedOnCamBehaviour();
        }

        public void SetKart(GameObject kart)
        {
            cinemachine.Follow = kart.transform;
            cinemachine.LookAt = kart.transform;

            // Remove (un-listen) old kart events
            if (_kartEvents != null)
            {
                _kartEvents.OnDriftBoostStart -= BoostCameraBehaviour;
                _kartEvents.OnBackCameraStart -= BackCamera;
                _kartEvents.OnBackCameraEnd -= BackCamera;
                //  _kartEvents.OnCameraTurnStart -= TurnCamera;
                _kartEvents.OnCameraTurnReset -= CameraReset;
            }

            _kartEngine = kart.GetComponentInChildren<KartEngine>();
            _kartEvents = kart.GetComponent<KartEvents>();

            // Add (listen) new kart events
            _kartEvents.OnDriftBoostStart += BoostCameraBehaviour;
            _kartEvents.OnBackCameraStart += BackCamera;
            _kartEvents.OnBackCameraEnd += BackCamera;
            //  _kartEvents.OnCameraTurnStart += TurnCamera;
            _kartEvents.OnCameraTurnReset += CameraReset;
        }

        #region CameraMovements

        public void BoostCameraBehaviour()
        {
            if (cameraBoostCoroutine != null)
                StopCoroutine(cameraBoostCoroutine);
            cameraBoostCoroutine = StartCoroutine(CameraBoostBehaviour(-8.5f, -MaxDistanceCamInBoost, 0.5f));
            currentTimer = 0f;
        }

        public void SpeedOnCamBehaviour()
        {
            float clampCam = Mathf.Clamp(_kartEngine.PlayerVelocity / 5, 0, 20);
            cinemachine.m_Lens.FieldOfView = 50 + clampCam;
        }

        public void AimAndFollow(bool value)
        {
            if (value)
            {
                cinemachine.AddCinemachineComponent<CinemachineComposer>();
                composer = cinemachine.GetCinemachineComponent<CinemachineComposer>();
                composer.m_ScreenY = 0.75f;
                composer.m_SoftZoneHeight = 0f;
                composer.m_SoftZoneWidth = 0f;
                composer.m_LookaheadSmoothing = 3f;
            }
            else
            {
                if (!backCamActivated || cameraBoostCoroutine != null)
                {
                    cinemachine.DestroyCinemachineComponent<CinemachineComposer>();
                    IonBeamInputs.IonBeamControlMode = true;
                }
            }
        }

        public void CameraReset()
        {
            orbiter.m_XAxis.Value = 0;
        }

        public void BackCamera(bool activate)
        {
            if (activate)
            {
                transposer.m_FollowOffset.z = 9;
                backCamActivated = true;
            }
            else
            {
                transposer.m_FollowOffset.z = -8.5f;
                backCamActivated = false;
            }
        }

        #endregion

        #region CameraTurn
        /*
        public void TurnCamera(float value)
        {
            if (Mathf.Abs(orbiter.m_XAxis.Value) >= 1f)
                orbiter.m_RecenterToTargetHeading.m_enabled = true;
            else
                orbiter.m_RecenterToTargetHeading.m_enabled = false;
        }
        */
        #endregion

        IEnumerator CameraBoostBehaviour(float startValue, float endValue, float boostDuration)
        {
            float startDynamicCamValue = transposer.m_FollowOffset.z;

            currentTimer = 0f;
            while (currentTimer < boostDuration)
            {
                if (!backCamActivated)
                {
                    transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValue, endValue, currentTimer / boostDuration);
                    currentTimer += Time.deltaTime;
                    yield return null;
                }
                else
                    break;
            }
            yield return new WaitForSeconds(1f);

            currentTimer = 0f;
            while (currentTimer < (boostDuration * 5))
            {
                if (!backCamActivated)
                {
                    transposer.m_FollowOffset.z = Mathf.Lerp(endValue, startValue, currentTimer / (boostDuration * 5));
                    currentTimer += Time.deltaTime;
                    yield return null;
                }
                else
                    break;
            }
        }
    }
}
