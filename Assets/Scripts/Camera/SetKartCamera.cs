using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class SetKartCamera : MonoBehaviour, IObserver
    {
        private GameObject _kartToFollow;
        private CinemachineVirtualCamera _cinemachine;

        // CORE

        private void Awake()
        {
            _cinemachine = GetComponent<CinemachineVirtualCamera>();
            if (_kartToFollow) SetKart(_kartToFollow);
        }

        // PUBLIC

        public void Observe(GameObject gameObject)
        {
            SetKart(gameObject);
        }

        public void SetKart(GameObject kart)
        {
            if (_kartToFollow != null)
            {
                _kartToFollow.GetComponent<AudioListener>().enabled = false;
            }
            _cinemachine.Follow = kart.transform;
            _cinemachine.LookAt = kart.transform;
            kart.GetComponent<AudioListener>().enabled = true;
            _kartToFollow = kart;
        }
    }
}
