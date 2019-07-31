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

        public void Attribution(int ID, string Text, MultiplePanelManager panel)
        {
            _textField.text = Text;

            _IDButton = ID;
            _manager = panel;
        }

        public void SetAction()
        {
            _manager.ButonAction(_IDButton);
        }
    }
}

