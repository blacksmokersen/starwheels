using UnityEngine;

namespace Gamemodes
{
    public class TotemSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _totemPickedAudioSource;
        [SerializeField] private AudioSource _totemLostAudioSource;

        public void PlayTotemPickedUpSound()
        {
            _totemPickedAudioSource.Play();
        }

        public void PlayTotemLostSound()
        {
            _totemLostAudioSource.Play();
        }
    }
}
