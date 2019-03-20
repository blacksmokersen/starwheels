using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindTotemForCam : MonoBehaviour
{
    [SerializeField] float _maxZoomOnTotem;

    private CinemachineVirtualCamera _cinemachine;
    private CinemachineTransposer _transposer;
    private CinemachineComposer _composer;

    private BoltEntity _totem;

    private bool _totemFound = false;

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

    private void FixedUpdate()
    {
        if (_totemFound && _totem.isAttached)
        {
            FollowTotemCamEffects();
        }
    }

    private void FollowTotemCamEffects()
    {
        var totemSpeed = (_maxZoomOnTotem + Mathf.Abs(_totem.GetComponent<Rigidbody>().velocity.magnitude));
        _transposer.m_FollowOffset.z = -totemSpeed;
    }

    IEnumerator FindTotemRoutine()
    {
        yield return new WaitForSeconds(5);

        if (GameObject.FindWithTag("Totem").transform != null)
        {
            _totem = GameObject.FindWithTag("Totem").GetComponent<BoltEntity>();
            _cinemachine.Follow = _totem.transform;
            _cinemachine.LookAt = _totem.transform;
            _totemFound = true;
        }

    }
}
