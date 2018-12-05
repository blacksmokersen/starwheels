using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Bolt;
using CameraUtils;

public class IonBeamCamera : EntityBehaviour
{
    [HideInInspector] public CinemachineTransposer Transposer;
    [HideInInspector] public CinemachineComposer Composer;

    [SerializeField] private float _speedCamMovements;
    [SerializeField] private float _ionBeamCamZExpand;
    [SerializeField] private float _ionBeamCamYExpand;
    [SerializeField] CameraSettings _cameraSettings;
    [SerializeField] private Texture2D _crosshairIonBeam;

    private CinemachineVirtualCamera _cinemachine;
    private Coroutine _cameraIonBeamBehaviour;

    private float _currentTimer;
    private bool _showCrosshair;
    private bool _isCameraOnTop = false;

    //CORE

    private void Awake()
    {
        _cinemachine = GetComponent<CinemachineVirtualCamera>();
        Transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        Composer = _cinemachine.GetCinemachineComponent<CinemachineComposer>();
    }

    //PUBLIC

    public void IonBeamCameraControls(float horizontal, float vertical)
    {
        Transposer.m_FollowOffset.z += horizontal * _speedCamMovements * Time.deltaTime;
        Transposer.m_FollowOffset.x += vertical * _speedCamMovements * Time.deltaTime;
    }

    public void IonBeamCameraBehaviour(bool direction)
    {
        if (direction)
        {
            if (_cameraIonBeamBehaviour != null)
                StopCoroutine(_cameraIonBeamBehaviour);
            _cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamExpand(_ionBeamCamZExpand, _ionBeamCamYExpand, 1f));
        }
        else
        {
            if (_cameraIonBeamBehaviour != null)
                StopCoroutine(_cameraIonBeamBehaviour);
            _cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(_cameraSettings.BaseCamPosition.z, _cameraSettings.BaseCamPosition.y, 0.5f));
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
            float xMin = (Screen.width / 2) - (_crosshairIonBeam.width / 2);
            float yMin = (Screen.height / 2) - (_crosshairIonBeam.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, _crosshairIonBeam.width, _crosshairIonBeam.height), _crosshairIonBeam);
        }
    }

    private void ChangeRenderOnTaGGameobjects(bool state)
    {
        GameObject[] TaggedGameobjectsToIgnore;
        TaggedGameobjectsToIgnore = GameObject.FindGameObjectsWithTag("IonBeamCamIgnore");

        foreach(GameObject GoToIgnore in TaggedGameobjectsToIgnore)
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
        float startDynamicCamValueZ = Transposer.m_FollowOffset.z;
        float startDynamicCamValueY = Transposer.m_FollowOffset.y;

        _currentTimer = 0f;
        while (_currentTimer < expandDuration)
        {
            Transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, endValueZ, _currentTimer / expandDuration);
            Transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, endValueY, _currentTimer / expandDuration);
            _currentTimer += Time.deltaTime;
            yield return null;
        }
        // transform.rotation = new Quaternion(Mathf.Lerp(transform.rotation.x, 90, _currentTimer / boostDuration), 0, 0, 0);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
        ChangeRenderOnTaGGameobjects(false);
        Composer.enabled = false;
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
        if (Transposer.m_FollowOffset.y > returnValueY)
        {
            // Security for lack of precision of Time.deltaTime
            _cameraIonBeamBehaviour = StartCoroutine(CameraIonBeamReset(returnValueZ, returnValueY, 0.5f));
        }
        ChangeRenderOnTaGGameobjects(true);
    }
}
