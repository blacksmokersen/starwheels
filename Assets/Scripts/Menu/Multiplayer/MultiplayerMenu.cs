﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MultiplayerMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private Button createRoomButton;

        [SerializeField] private Button backButton;

        [SerializeField] private GameObject panelRoomList;
        [SerializeField] private GameObject rowRoomPrefab;

        [SerializeField] private RoomMenu roomMenu;

        private bool _canCreateRoom = false;

        // CORE

        private void Awake()
        {
            createRoomButton.onClick.AddListener(
                () => FindObjectOfType<StringInput>().GetStringInput("Room name", CreateRoom)
            );
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            _canCreateRoom = true;
        }

        public override void OnJoinedLobby()
        {
            StopLoading();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            RefreshRooms(roomList);
        }

        public override void OnJoinedRoom()
        {
            backButton.interactable = false;
            roomMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);

            roomMenu.Refresh();
        }

        // PUBLIC

        public void ConnectToPhoton()
        {
            StartLoading();
            InitializePhoton();
        }

        public void DisconnectFromPhoton()
        {
            ClearRooms();
            PhotonNetwork.Disconnect();
        }

        // PRIVATE
        private void InitializePhoton()
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1.01";
            PhotonNetwork.ConnectUsingSettings();
        }

        private void StartLoading()
        {
            createRoomButton.interactable = false;
            loadingPanel.SetActive(true);
        }

        private void StopLoading()
        {
            createRoomButton.interactable = true;
            loadingPanel.SetActive(false);
        }

        private void CreateRoom(string roomName)
        {
            if (!_canCreateRoom) return;

            RoomOptions options = new RoomOptions() { MaxPlayers = 20 };
            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        private void ClearRooms()
        {
            for (int i = 1; i < panelRoomList.transform.childCount; ++i)
            {
                Destroy(panelRoomList.transform.GetChild(i).gameObject);
            }
        }

        private void RefreshRooms(List<RoomInfo> roomList)
        {
            ClearRooms();

            foreach (RoomInfo room in roomList)
            {
                var rowRoom = Instantiate(rowRoomPrefab, panelRoomList.transform).GetComponent<RowRoom>();
                rowRoom.UpdateRoomName(room.Name);
                rowRoom.UpdatePlayerCount(room.PlayerCount, room.MaxPlayers);
            }
        }
    }
}