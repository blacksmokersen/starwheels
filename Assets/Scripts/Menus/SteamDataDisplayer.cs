using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using SWExtensions;

namespace Menu
{
    public class SteamDataDisplayer : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image _playerProfilePicturePlaceHolder;
        [SerializeField] private Image _playerProfilePicture;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _experience;
        
        private int _avatar = -1;
        private Callback<AvatarImageLoaded_t> _avatarLoadedCallback;

        //CORE

        private void Start()
        {
            if (SteamManager.Initialized)
            {
                _avatarLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarLoaded);
                
                InitializeProfilePicture();
                InitializeName();
                InitializeExperience();
            }
        }

        // PRIVATE

        private void InitializeProfilePicture()
        {
            _avatar = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());
            if (_avatar > 0)
            {
                SetAvatarImage(_avatar);
            }
        }

        private void InitializeName()
        {
            _playerName.text = SteamFriends.GetPersonaName();
        }

        private void InitializeExperience()
        {
            int experience = 0;
            SteamUserStats.GetStat(Constants.SteamStats.Experience, out experience);
            _experience.text = "" + experience.ToString();
        }

        private void SetAvatarImage(int iImage)
        {
            _playerProfilePicturePlaceHolder.gameObject.SetActive(false);
            _playerProfilePicture.gameObject.SetActive(true);

            Rect rect = new Rect(0, 0, 184, 184);
            Vector2 pivot = new Vector2(.5f, .5f);
            Texture2D avatarTexture = SteamExtensions.GetSteamImageAsTexture2D(iImage);
            _playerProfilePicture.sprite = Sprite.Create(avatarTexture, rect, pivot);
        }

        private void OnAvatarLoaded(AvatarImageLoaded_t result)
        {
            SetAvatarImage(result.m_iImage);
        }
    }
}
