using UnityEngine;
using UnityEngine.UI;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class GamePrioritySetter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private MatchmakingSettings _settings;

        [Header("UI Elements")]
        [SerializeField] private Toggle _toggle;

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(SetStartedGamePriority);
            SetStartedGamePriority(_toggle.isOn);
        }

        private void SetStartedGamePriority(bool value)
        {
            _settings.LookForStartedGames = value;
        }
    }
}
