using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class MusicScript : MonoBehaviour
    {
        [SerializeField] private Button muteButton;

        private static MusicScript instance = null;

        // CORE

        private void Awake()
        {
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
            var audioSource = GetComponent<AudioSource>();
            audioSource.mute = !audioSource.mute;
        }
    }
}
