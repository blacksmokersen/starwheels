using System.Collections;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    public EngineSettings Settings;
    /*
    public IEnumerator Boost(float boostDuration, float magnitudeBoost, float speedBoost)
    {
        MaxMagnitude = Mathf.Clamp(MaxMagnitude, 0, _controlMagnitude) + magnitudeBoost;
        Speed = Mathf.Clamp(Speed, 0, _controlSpeed) + speedBoost;

        _currentTimer = 0f;
        while (_currentTimer < boostDuration)
        {
            _rb.AddRelativeForce(Vector3.forward * BoostPowerImpulse, ForceMode.VelocityChange);
            _currentTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _currentTimer = 0f;
        while (_currentTimer < boostDuration)
        {
            MaxMagnitude = Mathf.Lerp(_controlMagnitude + magnitudeBoost, _controlMagnitude, _currentTimer / boostDuration);
            Speed = Mathf.Lerp(_controlSpeed + speedBoost, _controlSpeed, _currentTimer / boostDuration);
            _currentTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    */
}
