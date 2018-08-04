using System.Collections;
using UnityEngine;
using XInputDotNetPure;

namespace Controls
{
    public class GamepadVibrations : BaseKartComponent
    {
        private new void Awake()
        {
            base.Awake();
            kartEvents.OnJump += MediumVibration;
            kartEvents.OnDoubleJump += (a) => MediumVibration();
            kartEvents.OnItemUsed += (a,b) => SmallVibration();
        }

        public IEnumerator Vibrate(float vibrationPower, float duration)
        {
            GamePad.SetVibration(0, vibrationPower, vibrationPower);
            yield return new WaitForSeconds(duration);
            ResetVibration();
        }

        public void ResetVibration()
        {
            GamePad.SetVibration(0, 0, 0);
        }

        private void SmallVibration()
        {
            StartCoroutine(Vibrate(0.1f, 0.5f));
        }

        private void MediumVibration()
        {
            StartCoroutine(Vibrate(0.3f, 0.6f));
        }

        private void StrongVibration()
        {
            StartCoroutine(Vibrate(0.5f, 1f));
        }

        private void OnApplicationQuit()
        {
            ResetVibration();
        }
    }
}