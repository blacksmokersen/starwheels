using UnityEngine;

namespace Audio
{
    public class MusicScript : MonoBehaviour
    {
        private static MusicScript instance = null;

        private AudioSource _audioSource;

        // CORE

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // PUBLIC

        public void Mute()
        {
            _audioSource.mute = !_audioSource.mute;
        }

        // PRIVATE
    }
}
