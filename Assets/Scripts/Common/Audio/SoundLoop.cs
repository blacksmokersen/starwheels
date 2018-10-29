using UnityEngine;


namespace Common.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundLoop : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<AudioSource>().loop = true;
        }

        public void Play()
        {
            GetComponent<AudioSource>().Play();
        }

        public void Stop()
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
