using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class InGameScoresEntry : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Image _abilityLogo;

        // PUBLIC

        public void UpdateScore(int score)
        {
            _scoreText.text = "" + score;
        }

        public void UpdateAbilityLogo(int index)
        {

        }
    }
}
