using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Cinemachine;

namespace CameraUtils
{
    public class CameraTarget : GlobalEventListener
    {
        [HideInInspector] public bool CameraTargetKart = false;

        [HideInInspector] public CinemachineVirtualCamera Cinemachine;

        private GameObject _actualTarget;
        private GameObject _mapSpecificCameraIntro;

        private GameObject _savedKart;

        // CORE

        private void Awake()
        {
            Cinemachine = GetComponent<CinemachineVirtualCamera>();
        }

        // BOLT

        public override void OnEvent(LobbyCountdown evnt)
        {
            if (_actualTarget = _savedKart)
            {
                SetCamToTargetThisGameobject(_mapSpecificCameraIntro);
            }
        }

        public override void OnEvent(GameReady evnt)
        {
            SetCamToFollowKart();
        }

        //PUBLIC

        public void SaveKart(GameObject kart)
        {
            _actualTarget = kart;
            _savedKart = kart;
        }

        public void SetCamToFollowKart()
        {
            GetComponent<SetKartCamera>().SetKart(_savedKart);
        }

        public void SetCamToTargetThisGameobject(GameObject target)
        {
            Cinemachine.Follow = target.transform;
            Cinemachine.LookAt = target.transform;
            _actualTarget = target;
            CameraTargetKart = false;
        }

        public void EmptyCamTarget()
        {
            Cinemachine.Follow = null;
            Cinemachine.LookAt = null;
            CameraTargetKart = false;
        }
    }
}
