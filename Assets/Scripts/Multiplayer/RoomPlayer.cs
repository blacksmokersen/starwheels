using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class RoomPlayer : UnityEngine.MonoBehaviour
{
    [SerializeField] private Text _playerNameText;

    public void SetPlayer(PhotonPlayer player)
    {
        _playerNameText.text = player.NickName;
    }
}
