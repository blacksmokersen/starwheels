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
            var resultingPitch = (minimumEnginePitch + maximumEnginePitch * pitch) / 27;

            if (resultingPitch > 0)
            {
                MotorFullSource.pitch = resultingPitch;
            }
            else
            {
              //  Debug.LogError("Pitch cannot be negative or null.");
                MotorFullSource.pitch = 0.5f;
            }
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
