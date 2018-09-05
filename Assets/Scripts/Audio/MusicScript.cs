using UnityEngine;

namespace Audio
{
    public class MusicScript : MonoBehaviour
    {
        public static MusicScript instance = null;

        [Header("OST")]
        public AudioSource TargetSource;

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
                TargetSource.Pause();
                muted = true;
            }
            else
            {
                TargetSource.UnPause();
                muted = false;
            }
        }
    }
}
