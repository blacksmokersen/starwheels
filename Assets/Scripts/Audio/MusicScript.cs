using UnityEngine;

namespace Audio
{
    public class MusicScript : MonoBehaviour
    {

        bool mute = false;

        static MusicScript instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                GameObject.DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Mute()
        {
            if (mute == false)
            {
                GetComponent<AudioSource>().Pause();
                mute = true;
            }
            else
            {
                GetComponent<AudioSource>().UnPause();
                mute = false;
            }
        }
    }
}
