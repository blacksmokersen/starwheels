using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SW.Matchmaking.Friends
{
    public class FriendEntry : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _profilePicture;

        public void UpdateName(string newName)
        {
            _name.text = newName;
        }

        public void UpdateProfilePicture(Texture2D texture)
        {
            //_profilePicture.sprite = new Sprite();
        }
    }
}
