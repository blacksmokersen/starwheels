using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CameraPlayerSwitch : MonoBehaviour
    {
        public bool CanOnlyWatchTeam = true;

        private CinemachineDynamicScript _cinemachineDynamicScript;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private GameObject _currentTarget;
        private Kart.KartEvents _currentKartEvents;

        private void Awake()
        {
            _cinemachineDynamicScript = GetComponent<CinemachineDynamicScript>();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetCameraToNextPlayer()
        {
            var newTargetKart = MyExtensions.Kart.GetNextTeamKart(_currentTarget);
            if (newTargetKart != null)
            {
                SetCameraToPlayer(newTargetKart);
            }
        }

        public void SetCameraToRandomPlayer()
        {
            var randomKart = MyExtensions.Kart.PickRandomTeamKart();
            if (randomKart != null)
            {
                SetCameraToPlayer(randomKart);
            }
        }

        private void SetCameraToPlayer(GameObject kart)
        {
            if (_currentTarget != null)
            {
                _currentTarget.GetComponentInChildren<Audio.KartSoundsScript>().SetAudioListenerActive(false);
            }
            _currentTarget = kart;
            _cinemachineDynamicScript.SetKart(_currentTarget);
            _currentKartEvents = _currentTarget.GetComponent<Kart.KartEvents>();
            _currentKartEvents.OnKartDestroyed += SetCameraToNextPlayer;
            _currentTarget.GetComponentInChildren<Audio.KartSoundsScript>().SetAudioListenerActive(true);

            FindObjectOfType<HUD.GameHUD>().ObserveKart(_currentTarget);            
        }
    }
}
