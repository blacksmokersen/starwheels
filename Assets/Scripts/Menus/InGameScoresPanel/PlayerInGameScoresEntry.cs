using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class PlayerInGameScoresEntry : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private TextMeshProUGUI _killCount;
        [SerializeField] private TextMeshProUGUI _deathCount;
        [SerializeField] private Image _abilityLogo;

        // PUBLIC

        public void UpdateNickname(string nickname)
        {
            _nickname.text = nickname;
        }

        public void UpdateKillCount(int killCount)
        {
            _killCount.text = "" + killCount;
        }

        public void UpdateDeathCount(int deathCount)
        {
            _deathCount.text = "" + deathCount;
        }

        public void UpdateAbilityLogo(int index)
        {

        }
    }
}
