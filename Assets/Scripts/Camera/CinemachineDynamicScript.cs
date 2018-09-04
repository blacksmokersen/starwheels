using System.Collections;
using UnityEngine;
using Cinemachine;
using Controls;
using Kart;

public class CinemachineDynamicScript : MonoBehaviour
{
    [Range(7.5f, 15)] public float MaxDistanceCamInBoost;
    public float SpeedCamMovements;

    private CinemachineVirtualCamera cinemachine;
    private Coroutine cameraBoostCoroutine;
    private Coroutine cameraIonBeamBehaviour;
    public CinemachineTransposer transposer;
    private CinemachineComposer composer;
    private CinemachineOrbitalTransposer orbiter;
    private bool backCamActivated = false;
    private bool cameraNeedReset = false;
    private float currentTimer;
    private KartEngine kartEngine;

    [SerializeField] private GameObject turnRotationPoint;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        orbiter = cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        transposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        composer = cinemachine.GetCinemachineComponent<CinemachineComposer>();
    }

    private void Start()
    {
        KartEvents.Instance.OnDriftBoost += BoostCameraBehaviour;
        KartEvents.Instance.OnBackCameraStart += BackCamera;
        KartEvents.Instance.OnBackCameraEnd += BackCamera;
        KartEvents.Instance.OnCameraTurnStart += TurnCamera;
        KartEvents.Instance.OnCameraTurnEnd +=  TurnOffCamera;
        KartEvents.Instance.OnCameraTurnReset += CameraReset;
    }

    private void Update()
    {
        SpeedOnCamBehaviour();
    }

    public void SetKart(GameObject kart)
    {
        cinemachine.Follow = kart.transform;
        cinemachine.LookAt = kart.transform;
        kartEngine = kart.GetComponentInChildren<KartEngine>();
    }

    public void IonBeamCameraControls(float horizontal, float vertical)
    {
        transposer.m_FollowOffset.z += horizontal * SpeedCamMovements * Time.deltaTime;
        transposer.m_FollowOffset.x += vertical * SpeedCamMovements * Time.deltaTime;
    }

    public void BoostCameraBehaviour()
    {
        if (cameraBoostCoroutine != null)
            StopCoroutine(cameraBoostCoroutine);
        cameraBoostCoroutine = StartCoroutine(CameraBoostBehaviour(-7.5f, -MaxDistanceCamInBoost, 0.5f));
        currentTimer = 0f;
    }

    public void SpeedOnCamBehaviour()
    {
        float clampCam = Mathf.Clamp(kartEngine.PlayerVelocity / 5, 0, 20);
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

    public void IonBeamCameraBehaviour(bool direction)
    {
        if (direction)
        {
            if (cameraIonBeamBehaviour != null)
                StopCoroutine(cameraIonBeamBehaviour);
            cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamExpand(0, 200, 1f));
        }
        else
        {
            AimAndFollow(true);
            if (cameraIonBeamBehaviour != null)
                StopCoroutine(cameraIonBeamBehaviour);
            cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(-7.5f, 3, 0.5f));
        }
    }

    public void TurnCamera(float value)
    {
        /*
        transposer.enabled = false;
        cinemachine.AddCinemachineComponent<CinemachineOrbitalTransposer>();
        orbiter = cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        orbiter.m_FollowOffset = new Vector3(0, 3,-7.5f);
        orbiter.m_XDamping = 0;
        orbiter.m_YDamping = 0;
        orbiter.m_ZDamping = 0;

        orbiter.m_XAxis.m_MaxSpeed = 150;
        orbiter.m_XAxis.m_AccelTime = 0;
        orbiter.m_XAxis.m_DecelTime = 0;
        orbiter.m_XAxis.m_MinValue = -50;
        orbiter.m_XAxis.m_MaxValue = 50;
        orbiter.m_XAxis.m_Wrap = false;
        orbiter.m_XAxis.m_InputAxisName = "RightJoystick";
        */



        /*
        composer.enabled = false;

        if (value != 0 && Mathf.Abs(transposer.m_FollowOffset.x) <= 8)
        {
            turnRotationPoint.transform.eulerAngles += new Vector3(0, -value* 100 * Time.deltaTime,0);
        }
        */

        /*
        if (value != 0 && Mathf.Abs(composer.m_TrackedObjectOffset.x) <= 8)
        {
            //   transposer.m_FollowOffset.x += value * 20 * Time.deltaTime;
            composer.m_TrackedObjectOffset += new Vector3(-value * 500 * Time.deltaTime,
                0,
                0);
            cameraNeedReset = true;
        }
        */

        /*
        if (value != 0 && Mathf.Abs(transposer.m_FollowOffset.x) <= 8)
            {
                //   transposer.m_FollowOffset.x += value * 20 * Time.deltaTime;
                transposer.m_FollowOffset += new Vector3(value * 20 * Time.deltaTime,
                    0,
                    Mathf.Abs(value) * 20 * Time.deltaTime);
                cameraNeedReset = true;
            }
            */
        }

    public void TurnOffCamera()
    {
        /*
        composer.enabled = true;
        */

        /*
        composer.m_TrackedObjectOffset.x = Mathf.Lerp(composer.m_TrackedObjectOffset.x, 0, Time.deltaTime * 10);
        composer.m_TrackedObjectOffset.z = Mathf.Lerp(composer.m_TrackedObjectOffset.z, 0, Time.deltaTime * 10);
        */

        /*
        transposer.m_FollowOffset.x = Mathf.Lerp(transposer.m_FollowOffset.x, 0, Time.deltaTime * 2);
        transposer.m_FollowOffset.z = Mathf.Lerp(transposer.m_FollowOffset.z, -7.5f, Time.deltaTime * 2);
        */
    }

    public void CameraReset()
    {
        Debug.Log("cameraRESET");
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
            transposer.m_FollowOffset.z = -7.5f;
            backCamActivated = false;
        }
    }

    IEnumerator CameraIonBeamExpand(float endValueZ, float endValueY, float boostDuration)
    {
        float startDynamicCamValueZ = transposer.m_FollowOffset.z;
        float startDynamicCamValueY = transposer.m_FollowOffset.y;

        currentTimer = 0f;
        while (currentTimer < boostDuration)
        {
            transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, endValueZ, currentTimer / boostDuration);
            transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, endValueY, currentTimer / boostDuration);
            currentTimer += Time.deltaTime;
            yield return null;
        }
        AimAndFollow(false);
    }

    IEnumerator CameraIonBeamReset(float returnValueZ, float returnValueY, float boostDuration)
    {
        float startDynamicCamValueX = transposer.m_FollowOffset.x;
        float startDynamicCamValueZ = transposer.m_FollowOffset.z;
        float startDynamicCamValueY = transposer.m_FollowOffset.y;

        currentTimer = 0f;

        while (currentTimer < boostDuration)
        {
            transposer.m_FollowOffset.x = Mathf.Lerp(startDynamicCamValueX, 0, currentTimer / boostDuration);
            transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, returnValueZ, currentTimer / boostDuration);
            transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, returnValueY, currentTimer / boostDuration);
            currentTimer += Time.deltaTime;
            yield return null;
        }
        if (transposer.m_FollowOffset.y > returnValueY)
        {
            // Security for lack of precision of Time.deltaTime
            cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(returnValueZ, returnValueY, 0.5f));
        }
    }

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
