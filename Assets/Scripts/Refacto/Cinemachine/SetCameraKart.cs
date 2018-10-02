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
    }
}
