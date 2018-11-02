using UnityEngine;
using Cinemachine;
using Multiplayer;
using MyExtensions;

namespace CameraUtils
{
    public class CameraPlayerSwitch : MonoBehaviour
    {
        public bool CanOnlyWatchTeam = true;

        private SetKartCamera _setKartCamera;
        private GameObject _currentTarget;

        private void Awake()
        {
            _setKartCamera = GetComponent<SetKartCamera>();
        }

        public void SetCameraToNextPlayer()
        {
            var newTargetKart = Player.Me.GetNextTeamKart(_currentTarget);
            if (newTargetKart != null)
            {
                SetCameraToPlayer(newTargetKart);
            }
        }

        public void SetCameraToRandomPlayer()
        {
            var randomKart = Player.Me.PickRandomTeamKart();

            if (randomKart != null)
            {
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

            var localHUD = Player.Me.GetComponentInChildren<Items.ItemHUD>();
            localHUD.ObserveKart(_currentTarget);
        }
    }
}
