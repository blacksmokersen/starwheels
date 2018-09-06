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
            var actualTarget = _cinemachineDynamicScript.ActualTarget;
            var newTargetKart = MyExtensions.Functions.GetNextTeamKart(actualTarget);
            _cinemachineDynamicScript.SetKart(newTargetKart);
        }

        void SetCameraToRandomPlayer()
        {
            var randomKart = MyExtensions.Functions.PickRandomTeamKart();
            _cinemachineDynamicScript.SetKart(randomKart);
        }
    }
}
