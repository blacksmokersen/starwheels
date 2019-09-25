using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Bolt;
using TMPro;

public class TeamBattlePortrait : GlobalEventListener
{
    public Team PortraitTeam;
    [HideInInspector] public int PlayerBindedID;
    [HideInInspector] public int LifeCount = 0;
    [HideInInspector] public bool IsAlreadyBinded = false;

    [SerializeField] private Image _avatar;
    [SerializeField] private GameObject _jailed;
    [SerializeField] private GameObject _dead;
    [SerializeField] private Image _life;
    [SerializeField] private Sprite[] _lifesprites;

    private void Start()
    {
        _life.sprite = _lifesprites[5];
    }

    public void SetLifeDisplay(int lifeCount)
    {
        _life.sprite = _lifesprites[lifeCount];
    }

    public void Jail(bool value)
    {
        if (value)
        {
            _jailed.SetActive(true);
        }
        else
        {
            _jailed.SetActive(false);
        }

    }

    public void Kill()
    {
        _dead.SetActive(true);
    }
}
