using UnityEngine;
using UnityEngine.UI;

namespace SW.Matchmaking.Friends
{
    [DisallowMultipleComponent]
    public class FriendsButtonInitializer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _friendsButton;

        // CORE

        private void Start()
        {
            SetReference();
        }

        private void OnEnable()
        {
            SetReference();
        }

        // PRIVATE

        private void SetReference()
        {
            _friendsButton.onClick.RemoveAllListeners();
            _friendsButton.onClick.AddListener(FindObjectOfType<InviteFriends>().CreateSteamFriendsLobby);
        }
    }
}
