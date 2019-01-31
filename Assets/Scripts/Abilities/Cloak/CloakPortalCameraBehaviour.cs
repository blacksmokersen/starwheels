using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CloakPortalCameraBehaviour : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera _cinemachine;

    [SerializeField] private GameObject _originPortal;
    [SerializeField] private GameObject _targetPortal;

    private CinemachineCollider _collider;
    private CinemachineTransposer _transposer;

    private void Awake()
    {
        _collider = GetComponent<CinemachineCollider>();
        _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }



    public void PortalTransferCamAnimation(GameObject origin,GameObject target, float transferDuration)
    {
        _originPortal.transform.position = origin.transform.position;
        _targetPortal.transform.position = target.transform.position;

        StartCoroutine(PortalCameraRoutine(origin,target,transferDuration));

    }

    IEnumerator PortalCameraRoutine(GameObject origin, GameObject target, float transferDuration)
    {
        _collider.enabled = false;
        float startDynamicCamValueZ = _transposer.m_FollowOffset.z;
        float startDynamicCamValueY = _transposer.m_FollowOffset.y;

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
