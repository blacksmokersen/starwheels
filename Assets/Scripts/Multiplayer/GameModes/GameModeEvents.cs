using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameModes
{
    public class GameModeEvents : MonoBehaviour
    {
        public static GameModeEvents Instance;

        // Events



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
