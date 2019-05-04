using UnityEngine;
using Steamworks;

namespace Multiplayer
{
    public class SteamPseudoInitializer : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private PlayerSettings _settings;

        // CORE

        private void Awake()
        {
            _settings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        // PUBLIC

        public void SetSteamPseudo()
        {
            if (SteamManager.Initialized)
            {
                _settings.Nickname = SteamFriends.GetPersonaName();
                Debug.Log("Set Steam pseudo to : " + _settings.Nickname);
            }
            else
            {
                Debug.LogWarning("Can't set nickname if Steam is not initialized");
                _settings.Nickname = "John Doe #" + (int)Random.Range(1,1000);
            }
        }
    }
}
