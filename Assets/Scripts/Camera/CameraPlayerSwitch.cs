using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CameraPlayerSwitch : MonoBehaviour
    {
        public bool CanOnlyWatchTeam = true;

        private CinemachineDynamicScript _cinemachineDynamicScript;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private Kart.KartEvents _currentKartEvents;

        private void Awake()
        {
            _cinemachineDynamicScript = GetComponent<CinemachineDynamicScript>();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetCameraToNextPlayer()
        {
            Debug.Log("Destroying");
            var currentTarget = _cinemachineDynamicScript.CurrentTarget;
            var newTargetKart = MyExtensions.Functions.GetNextTeamKart(currentTarget);
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
            FindObjectOfType<HUD.GameHUD>().ObserveKart(kart);
        }
    }
}
