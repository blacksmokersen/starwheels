using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CloakPortalCameraBehaviour : MonoBehaviour
{

    private CinemachineVirtualCamera _cinemachine;

    private GameObject _originPortal;
    private GameObject _targetPortal;

    private GameObject _targetToFollow;

    private CinemachineCollider _collider;
    private CinemachineTransposer _transposer;



    private void Awake()
    {
        _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
        _collider = GetComponent<CinemachineCollider>();
        _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }



    public void PortalTransferCamAnimation(GameObject origin,GameObject target, float transferDuration)
    {
        _originPortal.transform.position = origin.transform.position;
        _targetPortal.transform.position = target.transform.position;

      //  StartCoroutine(PortalCameraRoutine(origin,target,transferDuration));
    }

    public void StartPortalTransferCamAnimation(GameObject targetToFollow)
    {
        _cinemachine.Follow = targetToFollow.transform;
        _cinemachine.LookAt = targetToFollow.transform;

        _collider.enabled = false;
        _transposer.m_FollowOffset.y += 50;
    }

    public void StopPortalTransferCamAnimation(GameObject kartCameraOwner)
    {
        _cinemachine.Follow= kartCameraOwner.transform;
        _cinemachine.LookAt = kartCameraOwner.transform;
        _collider.enabled = true;
        _transposer.m_FollowOffset.y -= 50;
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
