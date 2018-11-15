using UnityEngine;

namespace Engine
{
    public class EngineSounds : MonoBehaviour
    {
        [Header("Engine")]
        public AudioSource MotorFullSource;

        [SerializeField] private float minimumEnginePitch;
        [SerializeField] private float maximumEnginePitch;

        private void Awake()
        {
            PlayMotorFullSound();
        }

        public void SetMotorFullPitch(float pitch)
        {
            MotorFullSource.pitch = (minimumEnginePitch + maximumEnginePitch * pitch) / 27;
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
}
