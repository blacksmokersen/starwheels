using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LobbyRoom : MonoBehaviour
{
    [SerializeField] private Text _serverNameText;
    [SerializeField] private Text _serverNameOwner;
    [SerializeField] private Button _joinServerBtn;

    public void SetServerName(RoomInfo room, UnityAction callback)
    {
        _serverNameText.text = room.Name;

        object owner;
        if (room.CustomProperties.TryGetValue("owner", out owner))
            _serverNameOwner.text = (string)owner;

        _joinServerBtn.onClick.AddListener(callback);
    }
}
