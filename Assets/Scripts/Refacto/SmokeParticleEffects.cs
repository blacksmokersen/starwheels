using UnityEngine;

public class SmokeParticleEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeLeftWheel;
    [SerializeField] private ParticleSystem smokeRightWheel;

    public void StopSmoke()
    {
        smokeLeftWheel.Stop(true);
        smokeRightWheel.Stop(true);
    }
    public void StartSmoke()
    {
        if (!smokeLeftWheel.isPlaying)
            smokeLeftWheel.Play(true);
        if (!smokeRightWheel.isPlaying)
            smokeRightWheel.Play(true);
    }

    public void SetWheelsColor(Color color)
    {
        var leftWheelMain = smokeLeftWheel.main;
        var rightWheelMain = smokeRightWheel.main;

        leftWheelMain.startColor = color;
        rightWheelMain.startColor = color;
    }
}
