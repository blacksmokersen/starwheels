using UnityEngine;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {

        static SoundManager instance = null;

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
    }
}
