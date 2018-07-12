using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

namespace Multiplayer
{
    public class Lobby : UnityEngine.MonoBehaviour
    {
        [SerializeField] private InputField _roomNameInput;
        [SerializeField] private InputField _nicknameInput;
        [SerializeField] private Button _createRoomBtn;
        [SerializeField] private Button _refreshRoomsBtn;
        [SerializeField] private LobbyRoom _lobbyRoomPrefab;
        [SerializeField] private Transform _roomList;

        private List<LobbyRoom> _lobbyRooms;

        private void Awake()
        {
            _refreshRoomsBtn.onClick.AddListener(RefreshLobby);
            _createRoomBtn.onClick.AddListener(CreateRoom);
            
            _nicknameInput.onValueChanged.AddListener((value) => {
                PhotonNetwork.playerName = value;
            });
            
            _lobbyRooms = new List<LobbyRoom>();

            Hide();
        }

        public void Show() { gameObject.SetActive(true); }
        public void Hide() { gameObject.SetActive(false); }
        
        private void CreateRoom()
        {
            string roomName = _roomNameInput.text;
            if (!string.IsNullOrEmpty(roomName))
            {
                RoomOptions options = new RoomOptions();
                options.MaxPlayers = 4;

                options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {
                    { "owner", PhotonNetwork.playerName },
                    { "map", 0 }
                };
                options.CustomRoomPropertiesForLobby = new string[] { "owner", "map"};
                
                PhotonNetwork.CreateRoom(roomName, options, null);
            }
        }

        public void RefreshLobby()
        {
            foreach (LobbyRoom item in _lobbyRooms)
            {
                Destroy(item.gameObject);
            }

            _lobbyRooms.Clear();

            foreach (RoomInfo room in PhotonNetwork.GetRoomList())
            {
                LobbyRoom item = Instantiate(_lobbyRoomPrefab);
                item.transform.SetParent(_roomList, false);
                string roomName = room.Name;
                item.SetServerName(room, () => {
                    PhotonNetwork.JoinRoom(roomName);
                });
                _lobbyRooms.Add(item);
            }
        }
    }
}