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
        [SerializeField] private Camera _freeCamera;
        [SerializeField] private CountdownSettings _countdownSettings;
        [SerializeField] private float _timeBeforeForcingPlayerCam;

        //CORE

        private void Start()
        {
            StartCoroutine(ForcePlayerCamera());

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
                    if (_freeCamera != null)
                    {
                        _freeCamera.enabled = false;
                        _freeCamera.GetComponent<FreeCamera>().Enabled = false;
                    }
                    else
                        Debug.LogWarning("There is no FreeCamera selected in the CameraSelector");
                }
                if (_kartCamera == null)
                    Debug.LogError("There is no PlayerCamera selected in the CameraSelector");
            }
            else
            {
                _kartCamera.enabled = true;
                if (_mapCamera != null)
                {
                    _mapCamera.enabled = false;
                    if (_freeCamera != null)
                    {
                        _freeCamera.enabled = false;
                        _freeCamera.GetComponent<FreeCamera>().Enabled = false;
                    }
                }
            }
        }

        //BOLT

        public override void OnEvent(OnAllPlayersInGame evnt)
        {
            if (evnt.IsGameAlreadyStarted)
                ShowPlayerCamera();
        }

        public override void OnEvent(LobbyCountdown evnt)
        {
            if (evnt.Time == 5)
            {
                ShowPlayerCamera();
            }
        }

        //PRIVATE

        private void ShowPlayerCamera()
        {
            _mapCamera.enabled = false;
            _freeCamera.enabled = false;
            _kartCamera.enabled = true;
            _freeCamera.GetComponent<FreeCamera>().Enabled = false;
            _freeCamera.GetComponent<FreeCamera>().EnableKartControls();
        }

        private void ShowMapCamera()
        {
            _mapCamera.enabled = true;
            _kartCamera.enabled = false;
            _freeCamera.enabled = false;
            _freeCamera.GetComponent<FreeCamera>().Enabled = false;
            _freeCamera.GetComponent<FreeCamera>().DisableKartControls();
        }

        private void ShowFreeCamera()
        {
            _mapCamera.enabled = false;
            _kartCamera.enabled = false;
            _freeCamera.enabled = true;
            _freeCamera.GetComponent<FreeCamera>().Enabled = true;
            _freeCamera.GetComponent<FreeCamera>().DisableKartControls();
        }

        //DEBUG

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                ShowPlayerCamera();
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                ShowMapCamera();
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                ShowFreeCamera();
            }
        }

        private IEnumerator ForcePlayerCamera()
        {
            yield return new WaitForSeconds(_timeBeforeForcingPlayerCam);
            ShowPlayerCamera();
        }
    }
}
