using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

namespace Menu
{
    public class SteamDataDisplayer : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image _playerProfilePicture;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _experience;
        [SerializeField] private TextMeshProUGUI _money;

        // CORE

        private void Start()
        {
            if (SteamManager.Initialized)
            {
                InitializeProfilePicture();
                InitializeName();
                InitializeExperience();
                InitializeMoney();
            }
        }

        // PRIVATE

        private void InitializeProfilePicture()
        {
            StartCoroutine(FetchAvatar());
        }

        private void InitializeName()
        {
            _playerName.text = SteamFriends.GetPersonaName();
        }

        private void InitializeExperience()
        {
            int experience = 0;
            SteamUserStats.GetStat(Constants.SteamStats.Experience, out experience);
            Debug.Log("Experience " + experience);
            _experience.text = "" + experience.ToString() + " xp";
        }

        private void InitializeMoney()
        {
            int money;
            SteamUserStats.GetStat(Constants.SteamStats.Money, out money);
            _money.text = "" + money.ToString() + " $";
        }

        private IEnumerator FetchAvatar()
        {
            var avatarInt = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());

            while (avatarInt == -1)
            {
                yield return null;
            }

            if (avatarInt > 0)
            {
                Debug.Log("Found avatar.");
                uint width, height;
                SteamUtils.GetImageSize(avatarInt, out width, out height);

                byte[] avatarStream = new byte[4 * (int)width * (int)height];
                if (width > 0 && height > 0)
                {
                    SteamUtils.GetImageRGBA(avatarInt, avatarStream, 4 * (int)width * (int)height);
                }

                Texture2D downloadedAvatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                downloadedAvatar.LoadRawTextureData(avatarStream);
                downloadedAvatar.Apply();

                Rect rect = new Rect(0, 0, 184, 184);
                Vector2 pivot = new Vector2(.5f, .5f);
                _playerProfilePicture.sprite = Sprite.Create(downloadedAvatar, rect, pivot);
                Debug.Log("Updated avatar.");
            }
            else
            {
                Debug.LogWarning("Couldn't fetch player avatar.");
            }
        }
    }
}
