using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Bolt;

public class IonBeamCamera : EntityBehaviour
{

    public float SpeedCamMovements;

    [HideInInspector] public CinemachineTransposer transposer;
    [HideInInspector] public CinemachineComposer composer;

    [SerializeField] private Texture2D crosshairIonBeam;
    private CinemachineVirtualCamera cinemachine;
    private Coroutine cameraIonBeamBehaviour;

    private float _currentTimer;
    private bool _showCrosshair;
    private bool _isCameraOnTop = false;

    //CORE

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        composer = cinemachine.GetCinemachineComponent<CinemachineComposer>();
    }

    //PUBLIC

    public void IonBeamCameraControls(float horizontal, float vertical)
    {
        transposer.m_FollowOffset.z += horizontal * SpeedCamMovements * Time.deltaTime;
        transposer.m_FollowOffset.x += vertical * SpeedCamMovements * Time.deltaTime;
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
            if (cameraIonBeamBehaviour != null)
                StopCoroutine(cameraIonBeamBehaviour);
            cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(-8.5f, 3, 0.5f));
        }
    }

    public bool IsCameraOnTop()
    {
        return _isCameraOnTop;
    }

    //PRIVATE

    private void OnGUI()
    {
        if (_showCrosshair)
        {
            float xMin = (Screen.width / 2) - (crosshairIonBeam.width / 2);
            float yMin = (Screen.height / 2) - (crosshairIonBeam.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairIonBeam.width, crosshairIonBeam.height), crosshairIonBeam);
        }
    }

    private void ChangeRenderOnTaGGameobjects(bool state)
    {
        if (GameObject.FindGameObjectWithTag("IonBeamCamIgnore") != null)
            GameObject.FindGameObjectWithTag("IonBeamCamIgnore").GetComponent<Renderer>().enabled = state;
    }

    IEnumerator CameraIonBeamExpand(float endValueZ, float endValueY, float boostDuration)
    {
        float startDynamicCamValueZ = transposer.m_FollowOffset.z;
        float startDynamicCamValueY = transposer.m_FollowOffset.y;

        _currentTimer = 0f;
        while (_currentTimer < boostDuration)
        {
            transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, endValueZ, _currentTimer / boostDuration);
            transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, endValueY, _currentTimer / boostDuration);
            _currentTimer += Time.deltaTime;
            yield return null;
        }
        // transform.rotation = new Quaternion(Mathf.Lerp(transform.rotation.x, 90, _currentTimer / boostDuration), 0, 0, 0);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
        ChangeRenderOnTaGGameobjects(false);
        composer.enabled = false;
        _showCrosshair = true;
        _isCameraOnTop = true;
    }

    IEnumerator CameraIonBeamReset(float returnValueZ, float returnValueY, float boostDuration)
    {
        _showCrosshair = false;
        _isCameraOnTop = false;
        float startDynamicCamValueX = transposer.m_FollowOffset.x;
        float startDynamicCamValueZ = transposer.m_FollowOffset.z;
        float startDynamicCamValueY = transposer.m_FollowOffset.y;

        _currentTimer = 0f;

        while (_currentTimer < boostDuration)
        {
            transposer.m_FollowOffset.x = Mathf.Lerp(startDynamicCamValueX, 0, _currentTimer / boostDuration);
            transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, returnValueZ, _currentTimer / boostDuration);
            transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, returnValueY, _currentTimer / boostDuration);
            _currentTimer += Time.deltaTime;
            yield return null;
        }
        if (transposer.m_FollowOffset.y > returnValueY)
        {
            // Security for lack of precision of Time.deltaTime
            cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(returnValueZ, returnValueY, 0.5f));
        }
        ChangeRenderOnTaGGameobjects(true);
    }
}
