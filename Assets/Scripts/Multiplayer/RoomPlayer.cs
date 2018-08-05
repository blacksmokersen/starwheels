using UnityEngine;
using UnityEngine.UI;

public class RoomPlayer : UnityEngine.MonoBehaviour
{
    [SerializeField] private Text _playerNameText;

    public void SetPlayer(PhotonPlayer player)
    {
        _playerNameText.text = player.NickName;
    }
}
