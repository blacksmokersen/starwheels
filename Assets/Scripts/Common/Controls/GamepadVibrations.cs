using System.Collections;
using UnityEngine;
using XInputDotNetPure;

namespace Common.Controls
{
    [DisallowMultipleComponent]
    public class GamepadVibrations : MonoBehaviour
    {
        // MONO

        private void OnEnable()
        {
            ResetVibration();
        }

        private void OnDisable()
        {
            ResetVibration();
        }

        private void OnApplicationQuit()
        {
            ResetVibration();
        }

        private void OnDestroy()
        {
            ResetVibration();
        }

        // PUBLIC

        [ContextMenu("Reset Vibration")]
        public void ResetVibration()
        {
            GamePad.SetVibration(0, 0, 0);
        }

        [ContextMenu("Small Vibration")]
        public void SmallVibration()
        {
            StartCoroutine(Vibrate(0.1f, 0.5f));
        }

        [ContextMenu("Medium Vibration")]
        public void MediumVibration()
        {
            StartCoroutine(Vibrate(0.3f, 0.6f));
        }

        [ContextMenu("Strong Vibration")]
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
    }
}
