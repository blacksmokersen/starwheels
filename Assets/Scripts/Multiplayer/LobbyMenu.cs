using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Multiplayer
{
    public class LobbyMenu : MonoBehaviourPunCallbacks
    {
        // HUD
        [SerializeField] private InputField roomNameInput;
        [SerializeField] private InputField nicknameInput;
        [SerializeField] private Button createRoomBtn;
        [SerializeField] private Button refreshRoomsBtn;
        [SerializeField] private Button backToMenu;

        [SerializeField] private LobbyRoom lobbyRoomPrefab;
        [SerializeField] private Transform roomListTransform;
        [SerializeField] private GameObject multiplayerMenu;

        private List<LobbyRoom> lobbyRooms;

        private void Awake()
        {
            //refreshRoomsBtn.onClick.AddListener(RefreshLobby);
            createRoomBtn.onClick.AddListener(CreateRoom);
            backToMenu.onClick.AddListener(BackToMenu);

            nicknameInput.onValueChanged.AddListener((value) => {
                PhotonNetwork.LocalPlayer.NickName = value;
            });

            lobbyRooms = new List<LobbyRoom>();

            HideLobbyMenu();
        }

        public void ShowLobbyMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideLobbyMenu()
        {
            gameObject.SetActive(false);
        }

        private void CreateRoom()
        {
            string roomName = roomNameInput.text;
            if (!string.IsNullOrEmpty(roomName))
            {
                RoomOptions options = new RoomOptions();
                options.MaxPlayers = 20;

                options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {
                    { "owner", PhotonNetwork.LocalPlayer.NickName },
                    { "map", 0 }
                };
                options.CustomRoomPropertiesForLobby = new string[] { "owner", "map"};

                PhotonNetwork.CreateRoom(roomName, options, null);
                PhotonNetwork.LoadLevel("Room");
            }
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            RefreshLobby(roomList);
        }

        public void RefreshLobby(List<RoomInfo> roomList)
        {
            // Clean all rooms
            foreach (LobbyRoom room in lobbyRooms)
            {
                Destroy(room.gameObject);
            }
            lobbyRooms.Clear();

            // Search and list all rooms
            foreach (RoomInfo roomInfo in roomList)
            {
                LobbyRoom room = Instantiate(lobbyRoomPrefab);
                room.transform.SetParent(roomListTransform, false);
                string roomName = roomInfo.Name;
                room.SetServerName(roomInfo, () => {
                    PhotonNetwork.JoinRoom(roomName);
                    //PhotonNetwork.LoadLevel("Room");
                });
                lobbyRooms.Add(room);
            }
        }

        public void BackToMenu()
        {
            multiplayerMenu.SetActive(false);
        }
    }
}
