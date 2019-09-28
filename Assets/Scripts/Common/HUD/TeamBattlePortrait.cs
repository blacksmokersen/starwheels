using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Bolt;
using TMPro;
using Steamworks;
using SWExtensions;

public class TeamBattlePortrait : GlobalEventListener
{
    [HideInInspector] public CSteamID SteamID;

    public Team PortraitTeam;
    [HideInInspector] public int PlayerBindedID;
    [HideInInspector] public int LifeCount = 0;
    [HideInInspector] public bool IsAlreadyBinded = false;

    [SerializeField] private Image _avatarImage;
    [SerializeField] private GameObject _jailed;
    [SerializeField] private GameObject _dead;
    [SerializeField] private Image _life;
    [SerializeField] private Sprite[] _lifesprites;

    private int _avatar = -1;
    private Callback<AvatarImageLoaded_t> _avatarLoadedCallback;

    private void Awake()
    {
        if (SteamManager.Initialized)
        {
            //   _avatarLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarLoaded);
        }
    }

    private void Start()
    {
        _life.sprite = _lifesprites[5];
    }

    public void UpdateAvatar(CSteamID steamUserID)
    {
        Debug.LogError("STEAM ID : " + steamUserID);
        if (SteamManager.Initialized)
        {
            _avatar = SteamFriends.GetLargeFriendAvatar(steamUserID);
            if (_avatar > 0)
            {
                SetAvatarImage(_avatar);
            }
        }
    }

    private void SetAvatarImage(int iImage)
    {
        //  _avatarPlaceholder.gameObject.SetActive(false);
        _avatarImage.gameObject.SetActive(true);

        Rect rect = new Rect(0, 0, 184, 184);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Texture2D avatarTexture = SteamExtensions.GetSteamImageAsTexture2D(iImage);
        _avatarImage.sprite = Sprite.Create(avatarTexture, rect, pivot);
    }

    /*
    private void OnAvatarLoaded(AvatarImageLoaded_t result)
    {
        if (result.m_steamID == SteamID)
        {
            SetAvatarImage(result.m_iImage);
        }
    }
    */

    public void SetLifeDisplay(int lifeCount)
    {
        if (LifeCount >= 0)
        {
            _life.sprite = _lifesprites[lifeCount];
        }
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
