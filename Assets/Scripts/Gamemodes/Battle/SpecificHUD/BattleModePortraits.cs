using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleModePortraits : MonoBehaviour
{
    [SerializeField] private Image _overrideImage;
    [SerializeField] private Image _outlineImage;
    [SerializeField] private Sprite _onJailSprite;
    [SerializeField] private Sprite _deathSprite;
    [SerializeField] private Color _normalColor = Color.blue;
    [SerializeField] private Color _deathColor = Color.black;
    public UnityEvent NormalEvent;
    public UnityEvent JailEvent;
    public UnityEvent DeathEvent;

    private void Start()
    {
        _overrideImage.gameObject.SetActive(false);
        _outlineImage.color = _normalColor;
        if (NormalEvent != null)
        {
            NormalEvent.Invoke();
        }
    }

    public void Jail(bool value)
    {
        if (value)
        {
            _overrideImage.sprite = _onJailSprite;
            if (JailEvent != null)
            {
                JailEvent.Invoke();
            }
        }
        else
        {
            if (NormalEvent != null)
            {
                NormalEvent.Invoke();
            }
        }
        _overrideImage.gameObject.SetActive(value);
    }

    public void Kill()
    {
        _overrideImage.sprite = _deathSprite;
        _outlineImage.color = _deathColor;
        _overrideImage.gameObject.SetActive(true);
        if (DeathEvent != null)
        {
            DeathEvent.Invoke();
        }
    }
}
