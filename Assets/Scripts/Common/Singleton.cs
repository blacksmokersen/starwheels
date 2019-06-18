using UnityEngine;

namespace Common
{
    [DisallowMultipleComponent]
    public class Singleton : MonoBehaviour
    {
        private static Singleton _instance;

        private void Awake()
        {
            if (_instance == null && _instance != this)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
