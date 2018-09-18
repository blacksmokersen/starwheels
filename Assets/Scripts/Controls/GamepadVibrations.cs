using System.Collections;
using UnityEngine;
using XInputDotNetPure;

namespace Controls
{
    public class GamepadVibrations : BaseKartComponent
    {
        // CORE

        private new void Awake()
        {
            base.Awake();

            kartEvents.OnJump += MediumVibration;
            kartEvents.OnDoubleJump += (a) => MediumVibration();
            kartEvents.OnItemUse += (a) => SmallVibration();
        }

        // PUBLIC

        public void ResetVibration()
        {
            if (!photonView.IsMine) return;

            GamePad.SetVibration(0, 0, 0);
        }

        public void SmallVibration()
        {
            if (!photonView.IsMine) return;

            StartCoroutine(Vibrate(0.1f, 0.5f));
        }

        public void MediumVibration()
        {
            if (!photonView.IsMine) return;

            StartCoroutine(Vibrate(0.3f, 0.6f));
        }

        public void StrongVibration()
        {
            if (!photonView.IsMine) return;

            StartCoroutine(Vibrate(0.5f, 1f));
        }

        // PRIVATE

        private IEnumerator Vibrate(float vibrationPower, float duration)
        {
            GamePad.SetVibration(0, vibrationPower, vibrationPower);
            yield return new WaitForSeconds(duration);
            ResetVibration();
        }

        private void OnApplicationQuit()
        {
            ResetVibration();
        }
    }
}
