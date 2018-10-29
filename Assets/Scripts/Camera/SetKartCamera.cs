using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

namespace CameraUtils
{
    public class SetKartCamera : MonoBehaviour
    {
        private GameObject _kartToFollow;

        private CinemachineVirtualCamera _cinemachine;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            //   if (kartToFollow) SetKart(kartToFollow);
        }
        /*
        private void Start()
        {
            if (!kartToFollow)
            {
                var kart = GameObject.FindGameObjectWithTag(Constants.Tag.Kart);
                if(kart) SetKart(kart);
            }
        }
        */
        /*
        private void KartAudioListenerBehaviour(GameObject kart)
        {
            if (_cinemachine.Follow.gameObject == kart)
            {

            }
        }
        */
        public void SetKart(GameObject kart)
        {
            if (_kartToFollow != null)
                _kartToFollow.GetComponent<AudioListener>().enabled = false;
            _cinemachine.Follow = kart.transform;
            _cinemachine.LookAt = kart.transform;
            kart.GetComponent<AudioListener>().enabled = true;
            _kartToFollow = kart;
        }
    }
}
