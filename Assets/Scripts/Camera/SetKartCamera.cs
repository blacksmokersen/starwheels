using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class SetKartCamera : MonoBehaviour
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
}
