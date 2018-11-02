using System.Collections;
using UnityEngine;
using XInputDotNetPure;

namespace Common.Controls
{
    public class GamepadVibrations : MonoBehaviour
    {
        // PUBLIC

        public void ResetVibration()
        {
            GamePad.SetVibration(0, 0, 0);
        }

        public void SmallVibration()
        {
            StartCoroutine(Vibrate(0.1f, 0.5f));
        }

        public void MediumVibration()
        {
            StartCoroutine(Vibrate(0.3f, 0.6f));
        }

        public void StrongVibration()
        {
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
