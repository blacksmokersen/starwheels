using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using SWExtensions;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class PlayerInGameScoresEntry : MonoBehaviour
    {
        [HideInInspector] public CSteamID SteamID;

        [Header("Settings")]
        [SerializeField] private int _maxNameLength;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private Image _avatarPlaceholder;
        [SerializeField] private Image _avatarImage;
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private Image _teamColor;
        [SerializeField] private Image _abilityLogo;
        [SerializeField] private TextMeshProUGUI _killCount;
        [SerializeField] private TextMeshProUGUI _deathCount;

        [SerializeField] private Sprite[] _allAbilitiesSprites;

        private GameSettings _gameSettings;

        private int _avatar = -1;
        private Callback<AvatarImageLoaded_t> _avatarLoadedCallback;

        //CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
            if (SteamManager.Initialized)
            {
                _avatarLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarLoaded);
            }
        }

        // PUBLIC

        public void UpdateRank(int rank)
        {
            _rank.text = "" + rank;
        }

        public void UpdateAvatar(CSteamID steamUserID)
        {
            if (SteamManager.Initialized)
            {
                _avatar = SteamFriends.GetLargeFriendAvatar(steamUserID);
                Debug.Log("Avatar : " + _avatar);
                if (_avatar > 0)
                {
                    SetAvatarImage(_avatar);
                }
            }
        }

        public void UpdateNickname(string nickname)
        {
            if (nickname.Length > _maxNameLength)
            {
                nickname = nickname.Substring(0, _maxNameLength);
            }
            _nickname.text = nickname;
        }

        public void UpdateTeamColor(Team team)
        {
            _teamColor.color = _gameSettings.TeamsListSettings.GetSettings(team).KillFeedEntryColor;
        }

        public void UpdateAbilityLogo(int index)
        {
            _abilityLogo.sprite = _allAbilitiesSprites[index];
        }

        public void UpdateKillCount(int killCount)
        {
            _killCount.text = "" + killCount;
        }

        public void UpdateDeathCount(int deathCount)
        {
            _deathCount.text = "" + deathCount;
        }

        // PRIVATE

        private void SetAvatarImage(int iImage)
        {
            _avatarPlaceholder.gameObject.SetActive(false);
            _avatarImage.gameObject.SetActive(true);

            Rect rect = new Rect(0, 0, 184, 184);
            Vector2 pivot = new Vector2(.5f, .5f);
            Texture2D avatarTexture = SteamExtensions.GetSteamImageAsTexture2D(iImage);
            _avatarImage.sprite = Sprite.Create(avatarTexture, rect, pivot);
        }

        private void OnAvatarLoaded(AvatarImageLoaded_t result)
        {
            Debug.Log("Avatar was loaded for user : " + result.m_steamID);
            if (result.m_steamID == SteamID)
            {
                Debug.Log("Hello");
                SetAvatarImage(result.m_iImage);
            }
        }
    }
}
