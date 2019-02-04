using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Cinemachine;

public class CloakPortalCameraBehaviour : EntityBehaviour<IKartState>
{

    private CinemachineVirtualCamera _cinemachine;

    private GameObject _originPortal;
    private GameObject _targetPortal;

    private GameObject _targetToFollow;

    private CinemachineCollider _collider;
    private CinemachineTransposer _transposer;

    private GameObject _cameraOwnerSave;
    private float _cameraDistanceInPortalSave;




    private void Awake()
    {
        _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
        _collider = GetComponent<CinemachineCollider>();
        _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }



    public void PortalTransferCamAnimation(GameObject origin, GameObject target, float transferDuration)
    {
        _originPortal.transform.position = origin.transform.position;
        _targetPortal.transform.position = target.transform.position;

        //  StartCoroutine(PortalCameraRoutine(origin,target,transferDuration));
    }

    public void StartPortalTransferCamAnimation(GameObject targetToFollow, GameObject cameraOwner, float cameraDistanceInPortal)
    {
        if (_cinemachine.Follow.gameObject == cameraOwner)
        {
            _cameraOwnerSave = cameraOwner;
            _cameraDistanceInPortalSave = cameraDistanceInPortal;

           // _cinemachine.Follow = targetToFollow.transform;
           // _cinemachine.LookAt = targetToFollow.transform;
            _cinemachine.Follow = null;
            _cinemachine.LookAt = null;

            GetComponentInParent<Transform>().position = targetToFollow.transform.position;

            _collider.enabled = false;
           // _transposer.m_FollowOffset.y += _cameraDistanceInPortalSave;
            _transposer.m_FollowOffset.y = 0;
            _transposer.m_FollowOffset.z = 0;

            transform.position = targetToFollow.transform.position;
            transform.rotation = targetToFollow.transform.rotation;
        }
    }

    public void StopPortalTransferCamAnimation(GameObject cameraOwner)
    {
        if (cameraOwner == _cameraOwnerSave)
        {
            _cinemachine.Follow = _cameraOwnerSave.transform;
            _cinemachine.LookAt = _cameraOwnerSave.transform;

            _transposer.m_FollowOffset.y = 2.2f;
            _transposer.m_FollowOffset.x = 0;
            _transposer.m_FollowOffset.z = -6.35f;

            _collider.enabled = true;
           // _transposer.m_FollowOffset.y -= _cameraDistanceInPortalSave;
        }
    }



    IEnumerator AnimationSwitchCameraToPortal(GameObject targetToFollow, float transferDuration)
    {
        _collider.enabled = false;
        //   float startDynamicCamValueZ = _transposer.m_FollowOffset.z;
        //  float startDynamicCamValueY = _transposer.m_FollowOffset.y;

        _transposer.m_FollowOffset.y = 5;

        var _currentTimer = 0f;
        while (_currentTimer < transferDuration)
        {


            // _transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValueZ, endValueZ, _currentTimer / transferDuration);
            // _transposer.m_FollowOffset.y = Mathf.Lerp(startDynamicCamValueY, endValueY, _currentTimer / transferDuration);
            _currentTimer += Time.deltaTime;
            yield return null;
        }
    }
}
