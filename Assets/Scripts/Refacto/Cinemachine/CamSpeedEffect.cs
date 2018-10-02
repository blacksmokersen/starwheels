using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSpeedEffect : MonoBehaviour {

    private CinemachineVirtualCamera _cinemachine;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    void Update ()
    {
        SpeedOnCamBehaviour();
    }

    public void SpeedOnCamBehaviour()
    {
        float clampCam = Mathf.Clamp(_rigidbody.velocity.magnitude / 5, 0, 20);
        _cinemachine.m_Lens.FieldOfView = 50 + clampCam;
    }
}
