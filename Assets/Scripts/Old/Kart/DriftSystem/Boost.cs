using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class Boost : MonoBehaviour
    {
        public BoostSettings Settings;





        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(_kartEngine.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));

            kartEvents.CallRPC("OnDriftBoostStart");
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
            kartEvents.CallRPC("OnDriftBoostEnd");
        }

    }
}
