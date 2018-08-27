using UnityEngine;
using UnityEngine.UI;

public class RoomPlayer : MonoBehaviour
{
    [SerializeField] private Text playerNameText;

    public void SetPlayer(PhotonPlayer player)
    {
        playerNameText.text = player.NickName;
    }
}
