using UnityEngine;

public class EngineSounds : MonoBehaviour
{
    [Header("Engine")]
    public AudioSource MotorAccelSource;
    public AudioSource MotorFullSource;
    public AudioSource MotorDecelSource;

    private void Awake()
    {
        PlayMotorFullSound();
    }

    public void SetMotorFullPitch(float pitch)
    {
        MotorFullSource.pitch = pitch;
    }

    private void PlayMotorFullSound()
    {
        MotorFullSource.Play();
    }

    private void StopMotorFullSound()
    {
        MotorFullSource.Stop();
    }
}
