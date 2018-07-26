using System.Collections;
using UnityEngine;
using XInputDotNetPure;

namespace Controls
{
    public class GamepadVibrations : MonoBehaviour
    {
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
    }
}