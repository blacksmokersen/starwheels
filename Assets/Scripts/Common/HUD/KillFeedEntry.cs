using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Common.HUD
{
    public class KillFeedEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killerName;
        [SerializeField] private Image _killerBackgroundPanel;
        [SerializeField] private Image _itemLogo;
        [SerializeField] private TextMeshProUGUI _victimName;
        [SerializeField] private Image _victimBackgroundPanel;

        private void Awake()
        {
            Destroy(gameObject, 5f);
        }

        public void SetKillerNameAndColor(string name, Color teamColor)
        {
            _killerName.text = name;
            _killerBackgroundPanel.color = teamColor;
        }

        public void SetItemIcon(Sprite itemLogo)
        {
            _itemLogo.sprite = itemLogo;
        }

        public void SetVictimNameAndColor(string name, Color teamColor)
        {
            _victimName.text = name;
            _victimBackgroundPanel.color = teamColor;
        }
    }
}
