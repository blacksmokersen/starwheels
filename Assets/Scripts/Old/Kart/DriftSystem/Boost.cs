using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

namespace Tools
{
    [RequireComponent(typeof(Rigidbody))]
    public class Boost : MonoBehaviourPun
    {
        public BoostSettings Settings;

        private Rigidbody _rigidBody;
        private Coroutine _turboCoroutine;
        private Coroutine _physicsBoostCoroutine;

        public Action OnDriftBoostStart;
        public Action OnDriftBoostEnd;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void StartTurbo()
        {
            _turboCoroutine = StartCoroutine(EnterTurbo());
        }

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(BoostPhysic(Settings.BoostDuration, Settings.MagnitudeBoost, Settings.BoostSpeed));

            CallRPC("OnDriftBoostStart");
            yield return new WaitForSeconds(Settings.BoostDuration);
            ResetDrift(); // LANCER EVENT RESET DRIFT ???
            CallRPC("OnDriftBoostEnd");
        }

        public IEnumerator BoostPhysic(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            Settings.MaxMagnitude = Mathf.Clamp(Settings.MaxMagnitude, 0, Settings._controlMagnitude) + magnitudeBoost;
            Settings.Speed = Mathf.Clamp(Settings.Speed, 0, Settings._controlSpeed) + speedBoost;

            Settings._currentTimer = 0f;
            while (Settings._currentTimer < boostDuration)
            {
                _rigidBody.AddRelativeForce(Vector3.forward * Settings.BoostPowerImpulse, ForceMode.VelocityChange);
                Settings._currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Settings._currentTimer = 0f;
            while (Settings._currentTimer < boostDuration)
            {
                Settings.MaxMagnitude = Mathf.Lerp(Settings._controlMagnitude + magnitudeBoost, Settings._controlMagnitude, Settings._currentTimer / boostDuration);
                Settings.Speed = Mathf.Lerp(Settings._controlSpeed + speedBoost, Settings._controlSpeed, Settings._currentTimer / boostDuration);
                Settings._currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        [PunRPC] public void RPCOnDriftBoostEnd() { OnDriftBoostEnd(); }
        [PunRPC] public void RPCOnDriftBoostStart() { OnDriftBoostStart(); }

        public void CallRPC(string onAction, params object[] parameters)
        {
            photonView.RPC("RPC" + onAction, RpcTarget.All, parameters);
        }
    }
}
