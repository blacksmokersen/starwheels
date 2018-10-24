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

        private void Start()
        {
            if (!kartToFollow)
            {
                var kart = GameObject.FindGameObjectWithTag(Constants.Tag.Kart);
                if(kart) SetKart(kart);
            }
        }

        public void SetKart(GameObject kart)
        {
            Debug.Log("Observing kart : " + kart.GetComponent<Multiplayer.PlayerSettings>().Nickname);
            _cinemachine.Follow = kart.transform;
            _cinemachine.LookAt = kart.transform;
        }
    }
}
