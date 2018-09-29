using UnityEngine;

[CreateAssetMenu(menuName = "Sounds/EngineSounds")]
public class NewBehaviourScript : ScriptableObject
{
    [Header("Engine")]
    public AudioSource MotorAccelSource;
    public AudioSource MotorFullSource;
    public AudioSource MotorDecelSource;

    private void SetMotorFullPitch(float pitch)
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
