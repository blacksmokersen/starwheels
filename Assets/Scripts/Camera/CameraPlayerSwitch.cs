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
            var newTargetKart = MyExtensions.Functions.GetNextTeamKart(_currentTarget);
            if (newTargetKart != null)
            {
                SetCameraToPlayer(newTargetKart);
            }
        }

        public void SetCameraToRandomPlayer()
        {
            var randomKart = MyExtensions.Functions.PickRandomTeamKart();
            if (randomKart != null)
            {
                SetCameraToPlayer(randomKart);
            }
        }

        private void SetCameraToPlayer(GameObject kart)
        {
            _cinemachineDynamicScript.SetKart(kart);
            _currentKartEvents = kart.GetComponent<Kart.KartEvents>();
            _currentKartEvents.OnKartDestroyed += SetCameraToNextPlayer;
            _currentTarget = kart;
            FindObjectOfType<HUD.GameHUD>().ObserveKart(kart);
        }
    }
}
