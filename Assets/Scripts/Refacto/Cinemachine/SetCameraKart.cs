using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraKart : MonoBehaviour
{

    private CinemachineVirtualCamera _cinemachine;


    private void Awake()
    {
        _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
    }
        public void SetKart(GameObject kart)
    {
        _cinemachine.Follow = kart.transform;
        _cinemachine.LookAt = kart.transform;


        /*
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
        */
    }
}
