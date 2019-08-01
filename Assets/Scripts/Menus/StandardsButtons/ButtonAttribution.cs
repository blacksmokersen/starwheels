using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Menu.Options
{
    public class ButtonAttribution : MonoBehaviour
    {
        private int _IDButton;
        private MultiplePanelManager _manager;

        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private Animator _buttonAnime;

        public void Attribution(int ID, string Text, MultiplePanelManager panel)
        {
            _textField.text = Text;

            _IDButton = ID;
            _manager = panel;
        }

        public void SetAction()
        {
            _manager.ButtonAction(_IDButton);
        }

        public void ActualizePosition()
        {
            _manager.OnMouseActualization(_IDButton);
        }

        public void HighlightButton(bool value)
        {
            if (value)
            {
                _buttonAnime.ResetTrigger("OnMouseExit");
                _buttonAnime.ResetTrigger("OnClick");
                _buttonAnime.SetTrigger("OnMouseEnter");
            }
            else
            {
                _buttonAnime.ResetTrigger("OnMouseEnter");
                _buttonAnime.SetTrigger("OnMouseExit");
            }
        }

        public void ValidationEffect()
        {
            _buttonAnime.ResetTrigger("OnMouseEnter");
            _buttonAnime.SetTrigger("OnClick");
        }
    }
}

