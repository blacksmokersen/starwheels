using UnityEngine;
using TMPro;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class LobbyPlayerInfoEntry : MonoBehaviour
    {
        [Header("Information")]
        public string Nickname;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _nicknameText;

        // PUBLIC

        public void SetNickname(string nickname)
        {
            Nickname = nickname;
            _nicknameText.text = nickname;
        }
    }
}
