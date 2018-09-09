using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class MusicScript : MonoBehaviour
    {
        [SerializeField] private Button muteButton;

        private static MusicScript instance = null;

        private AudioSource _audioSource;

        // CORE

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            muteButton.onClick.AddListener(ToggleMute);

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

        // PRIVATE

        private void ToggleMute()
        {
            _audioSource.mute = !_audioSource.mute;
        }
    }
}
