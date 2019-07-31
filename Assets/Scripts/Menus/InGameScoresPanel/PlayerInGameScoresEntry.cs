using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class PlayerInGameScoresEntry : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int _maxNameLength;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private Image _teamColor;
        [SerializeField] private Image _abilityLogo;
        [SerializeField] private TextMeshProUGUI _killCount;
        [SerializeField] private TextMeshProUGUI _deathCount;

        [SerializeField] private Sprite[] _allAbilitiesSprites;

        private GameSettings _gameSettings;

        //CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        // PUBLIC

        public void UpdateRank(int rank)
        {
            _rank.text = "" + rank;
        }

        public void UpdateAvatar(int userID)
        {
            Rect rect = new Rect(0, 0, 184, 184);
            Vector2 pivot = new Vector2(.5f, .5f);
            Texture2D avatarTexture = SWExtensions.SteamExtensions.GetSteamAvatarTexture((CSteamID)(ulong)userID);
            _avatar.sprite = Sprite.Create(avatarTexture, rect, pivot);
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
    }
}
