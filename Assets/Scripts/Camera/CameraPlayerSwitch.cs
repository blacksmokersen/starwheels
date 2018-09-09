using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CameraPlayerSwitch : MonoBehaviour
    {
        public bool CanOnlyWatchTeam = true;

        private CinemachineDynamicScript _cinemachineDynamicScript;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private void Awake()
        {
            _cinemachineDynamicScript = GetComponent<CinemachineDynamicScript>();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetCameraToNextPlayer()
        {
            var currentTarget = _cinemachineDynamicScript.CurrentTarget;
            var newTargetKart = MyExtensions.Functions.GetNextTeamKart(currentTarget);
            if (newTargetKart != null)
            {
                _cinemachineDynamicScript.SetKart(newTargetKart);
                FindObjectOfType<HUD.GameHUD>().ObserveKart(newTargetKart);
            }
        }

        public void SetCameraToRandomPlayer()
        {
            var randomKart = MyExtensions.Functions.PickRandomTeamKart();
            if (randomKart != null)
            {
                _cinemachineDynamicScript.SetKart(randomKart);
                FindObjectOfType<HUD.GameHUD>().ObserveKart(randomKart);
            }
        }
    }
}
