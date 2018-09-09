using UnityEngine;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance = null;

        // CORE

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

        // PUBLIC

        // PRIVATE
    }
}
