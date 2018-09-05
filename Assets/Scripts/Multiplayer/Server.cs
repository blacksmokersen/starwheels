using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Multiplayer
{
    public class Server : MonoBehaviourPunCallbacks
    {
        [SerializeField] private RoomMenu roomMenu;
        [SerializeField] private LobbyMenu lobbyMenu;

        private void Awake()
        {
            Debug.Log("Hello");
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene  = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            lobbyMenu.ShowLobbyMenu();
            //lobbyMenu.RefreshLobby();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            roomMenu.ShowRoomMenu();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("OnRoomListUpdate");
            lobbyMenu.RefreshLobby(roomList);
        }
    }
}
