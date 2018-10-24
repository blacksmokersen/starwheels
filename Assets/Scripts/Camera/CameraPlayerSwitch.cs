using UnityEngine;
using Cinemachine;
using Multiplayer;
using MyExtensions;

namespace CameraUtils
{
    public class CameraPlayerSwitch : MonoBehaviour
    {
        public bool CanOnlyWatchTeam = true;
        public PlayerSettingsSO PlayerSettingsSO;

        private SetKartCamera _setKartCamera;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private GameObject _currentTarget;

        private void Awake()
        {
            _setKartCamera = GetComponent<SetKartCamera>();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetCameraToNextPlayer()
        {
            var newTargetKart = PlayerSettings.Me.GetNextTeamKart(_currentTarget);
            if (newTargetKart != null)
            {
                SetCameraToPlayer(newTargetKart);
            }
        }

        public void SetCameraToRandomPlayer()
        {
            var randomKart = PlayerSettings.Me.PickRandomTeamKart();

            if (randomKart != null)
            {
                Debug.Log("Setting camera to new kart");
                SetCameraToPlayer(randomKart);
            }
        }

        private void SetCameraToPlayer(GameObject kart)
        {
            if (_currentTarget != null)
            {
                //_currentTarget.GetComponentInChildren<Audio.KartSoundsScript>().SetAudioListenerActive(false);
            }
            _currentTarget = kart;
            _setKartCamera.SetKart(_currentTarget);
            //_currentTarget.GetComponentInChildren<Audio.KartSoundsScript>().SetAudioListenerActive(true);

            var localHUD = PlayerSettings.Me.GetComponentInChildren<Items.ItemHUD>();
            localHUD.ObserveKart(_currentTarget);
        }
    }
}
