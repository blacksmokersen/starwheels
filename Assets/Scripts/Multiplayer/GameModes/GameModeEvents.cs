using System;
using UnityEngine;
using Photon.Pun.UtilityScripts;

namespace GameModes
{
    public class GameModeEvents : MonoBehaviour
    {
        public static GameModeEvents Instance;

        // Events
        public Action<PunTeams.Team> OnKartDestroyed;
        public System.Action OnGameReset;

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
