using UnityEngine;
using TMPro;
using SW.Abilities;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class CustomizationPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _explainationText;

        [Header("References")]
        [SerializeField] private CharacterSetter _characterSetter;
        [SerializeField] private KartMeshSetter _kartSetter;
        [SerializeField] private AbilityMenuSetter _abilitySetter;

        public void SetAbilityInfo()
        {
            _explainationText.text = _abilitySetter.GetCurrentAbilityDescription();
        }

        public void SetCharacterInfo()
        {
            _explainationText.text = _characterSetter.CurrentCharacter.GetComponent<CharacterMeshIntLabel>().CharacterName;
        }

        public void SetKartInfo()
        {
            var kartInfo = _kartSetter.CurrentKart.GetComponent<KartSkinSettings>();
            _explainationText.text = kartInfo.KartName + " : " + kartInfo.PassiveDescription;
        }
    }
}
