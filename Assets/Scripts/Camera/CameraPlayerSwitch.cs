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
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetCameraToNextPlayer()
        {
            var currentTarget = _cinemachineDynamicScript.CurrentTarget;
            var newTargetKart = MyExtensions.Kart.GetNextTeamKart(currentTarget);
            _cinemachineDynamicScript.SetKart(newTargetKart);
        }

        void SetCameraToRandomPlayer()
        {
            var randomKart = MyExtensions.Kart.PickRandomTeamKart();
            _cinemachineDynamicScript.SetKart(randomKart);
        }
    }
}
