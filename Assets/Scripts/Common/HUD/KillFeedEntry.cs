using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Common.HUD
{
    public class KillFeedEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killerName;
        [SerializeField] private Image _itemLogo;
        [SerializeField] private TextMeshProUGUI _victimName;

        public void SetKillerName(string name)
        {
            _killerName.text = name;
        }

        public void SetItemIcon(Image itemLogo)
        {
            _itemLogo = itemLogo;
        }

        public void SetVictimName(string name)
        {
            _victimName.text = name;
        }
    }
}
