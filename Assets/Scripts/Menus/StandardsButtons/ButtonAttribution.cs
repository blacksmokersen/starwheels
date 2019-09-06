using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Menu.Options
{
    public class ButtonAttribution : MonoBehaviour
    {
        [SerializeField] private int _IDButton;
        [SerializeField] private MenuGeneration _managerGeneration;
        [SerializeField] private MenuNavigation _managerNavigation;

        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private Animator _buttonAnime;
        [SerializeField] private Button _buttonSystem;

        public UnityEvent OnEvent;
        public UnityEvent OffEvent;
        public UnityEvent OpenInitEvent;

        private void OnEnable()
        {
            if (OpenInitEvent != null)
            {
                OpenInitEvent.Invoke();
            }
        }

        public void Attribution(int ID, string Text, MenuGeneration panel)
        {
            _textField.text = Text;

            _IDButton = ID;
            _managerGeneration = panel;
        }

        public void SetAction()
        {
            if (_managerGeneration)
            {
                _managerGeneration.ButtonAction(_IDButton);
            }
            else if (_managerNavigation)
            {
                _managerNavigation.ButtonAction(_IDButton);
            }
        }

        public void ActualizePosition()
        {
            if (_managerGeneration)
            {
                _managerGeneration.OnMouseActualization(_IDButton);
            }
            else if (_managerNavigation)
            {
                _managerNavigation.OnMouseActualization(_IDButton);
            }
        }

        public void HighlightButton(bool value)
        {
            if (_buttonAnime)
            {
                _buttonAnime.ResetTrigger("OnClick");
                _buttonAnime.SetBool("CursorOn", value);
            }

            if (value)
            {
                if (OnEvent != null)
                {
                    OnEvent.Invoke();
                }
            }
            else
            {
                if (OffEvent != null)
                {
                    OffEvent.Invoke();
                }
            }
        }

        public void ValidationEffect()
        {
            if (_buttonAnime)
            {
                _buttonAnime.SetTrigger("OnClick");
            }
        }

        public void EnableButtonInteraction(bool value)
        {
            if (_buttonSystem != null)
            {
                _buttonSystem.interactable = value;
            }
        }
    }
}

