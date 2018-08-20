using UnityEngine;

namespace Audio
{
    public class MusicScript : MonoBehaviour
    {
        static MusicScript instance = null;

        private bool muted = false;

        private void Awake()
        {
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

        public void Mute()
        {
            if (muted == false)
            {
                GetComponent<AudioSource>().Pause();
                muted = true;
            }
            else
            {
                GetComponent<AudioSource>().UnPause();
                muted = false;
            }
        }
    }
}
