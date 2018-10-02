using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameModes
{
    public class GameModeEvents : MonoBehaviour
    {
        public static GameModeEvents Instance;

        // Events
        public TeamEvent OnKartDestroyed;
        public UnityEvent OnGameReset;
        public TeamEvent OnGameEnd;
        public UnityEvent OnGameStart;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
