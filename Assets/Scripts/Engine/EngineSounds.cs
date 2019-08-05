using UnityEngine;

namespace Engine
{
    public class EngineSounds : MonoBehaviour
    {
        [Header("Engine")]
        public AudioSource MotorFullSource;

        [SerializeField] private float minimumEnginePitch;
        [SerializeField] private float maximumEnginePitch;

      //  [SerializeField] private float _basePitch;

        private void Awake()
        {
            PlayMotorFullSound();
        }

        public void SetMotorFullPitch(float speed)
        {
            var resultingPitch = (minimumEnginePitch + maximumEnginePitch * speed) / 27;

            if (resultingPitch > 0)
            {
                MotorFullSource.pitch = resultingPitch;
            }
            else
            {
                //  Debug.LogError("Pitch cannot be negative or null.");
                MotorFullSource.pitch = Mathf.Abs(resultingPitch);
             //   MotorFullSource.pitch = 0.5f;
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
