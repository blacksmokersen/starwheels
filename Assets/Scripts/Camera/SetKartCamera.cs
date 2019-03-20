using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class SetKartCamera : CameraTarget, IObserver
    {
        private GameObject _kartToFollow;

        // CORE

        private void Awake()
        {
            Cinemachine = GetComponent<CinemachineVirtualCamera>();
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
            Cinemachine.Follow = kart.transform;
            Cinemachine.LookAt = kart.transform;
            kart.GetComponent<AudioListener>().enabled = true;
            _kartToFollow = kart;
           // SaveKart(kart);
            CameraTargetKart = true;
        }
    }
}
