using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class SetKartCamera : MonoBehaviour
    {
        [SerializeField] private GameObject kartToFollow;

        private CinemachineVirtualCamera _cinemachine;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            if (kartToFollow) SetKart(kartToFollow);
        }

        public void SetKart(GameObject kart)
        {
            _cinemachine.Follow = kart.transform;
            _cinemachine.LookAt = kart.transform;
        }
    }
}
