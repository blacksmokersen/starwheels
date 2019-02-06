using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindTotemForCam : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachine;
    private CinemachineTransposer _transposer;
    private CinemachineComposer _composer;
 //   private CinemachineCollider _collider;

    private void Awake()
    {
        _cinemachine = GetComponent<CinemachineVirtualCamera>();
        _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        _composer = _cinemachine.GetCinemachineComponent<CinemachineComposer>();
       // _collider = GetComponent<CinemachineCollider>();
    }

    private void Start()
    {
        StartCoroutine(FindTotemRoutine());
    }


    IEnumerator FindTotemRoutine()
    {
        yield return new WaitForSeconds(3);

        if (GameObject.FindWithTag("Totem").transform != null)
        {
            _cinemachine.Follow = GameObject.FindWithTag("Totem").transform;
            _cinemachine.LookAt = GameObject.FindWithTag("Totem").transform;
        }

    }
}
