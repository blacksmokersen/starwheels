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

        private void Awake()
        {
            Destroy(gameObject, 5f);
        }

        public void SetKillerNameAndColor(string name, Color teamColor, bool highlight)
        {
            _killerName.text = name;
            if (highlight)
            {
                _killerName.fontStyle = FontStyles.Bold;
                _killerName.color = Color.yellow;
            }
            else
            {
                _killerName.color = teamColor;
            }
        }

        public void SetItemIcon(Sprite itemLogo)
        {
            _itemLogo.sprite = itemLogo;
        }

        public void SetVictimNameAndColor(string name, Color teamColor, bool highlight)
        {
            _victimName.text = name;
            if (highlight)
            {
                _victimName.fontStyle = FontStyles.Bold;
                _victimName.color = Color.yellow;
            }
            else
            {
                _victimName.color = teamColor;
            }

            GetComponent<RectTransform>().position.Set(0, 0, 0);
            GetComponent<RectTransform>().SetAsFirstSibling();
        }
    }
}
