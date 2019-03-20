using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace CameraUtils
{
    public class CameraSelector : GlobalEventListener
    {
        [SerializeField] private Camera _kartCamera;
        [SerializeField] private Camera _mapCamera;
        [SerializeField] private CountdownSettings _countdownSettings;

        //CORE

        private void Start()
        {
            if (BoltNetwork.IsConnected)
            {
                if (!_countdownSettings.Countdown)
                    ShowPlayerCamera();
                if (_mapCamera == null)
                {
                    Debug.LogWarning("There is no MapCamera selected in the CameraSelector");
                    _kartCamera.enabled = true;
                }
                else
                {
                    _mapCamera.enabled = true;
                    _kartCamera.enabled = false;
                }
                if (_kartCamera == null)
                    Debug.LogError("There is no PlayerCamera selected in the CameraSelector");
            }
            else
            {
                _kartCamera.enabled = true;
                if(_mapCamera != null)
                    _mapCamera.enabled = false;
            }
        }

        //BOLT

        public override void OnEvent(LobbyCountdown evnt)
        {
            if (evnt.Time == 3)
            {
                ShowPlayerCamera();
            }
        }

        //PRIVATE

        private void ShowPlayerCamera()
        {
            _mapCamera.enabled = false;
            _kartCamera.enabled = true;
        }

        private void ShowMapCamera()
        {
            _mapCamera.enabled = true;
            _kartCamera.enabled = false;
        }

        //DEBUG

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _mapCamera.enabled = false;
                _kartCamera.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                _mapCamera.enabled = true;
                _kartCamera.enabled = false;
            }
        }
    }
}
