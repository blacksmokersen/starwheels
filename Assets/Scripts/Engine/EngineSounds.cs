using UnityEngine;

namespace Engine
{
    public class EngineSounds : MonoBehaviour
    {
        [Header("Engine")]
        public AudioSource MotorFullSource;


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
}
