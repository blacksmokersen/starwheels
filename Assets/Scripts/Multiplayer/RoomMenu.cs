using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

namespace Multiplayer
{
    public class RoomMenu : PunBehaviour
    {
        // HUD
        [SerializeField] private Text roomTitleText;
        [SerializeField] private Button startButton;
        [SerializeField] private Dropdown mapDropdown;
        [SerializeField] private Dropdown teamDropdown;

        [SerializeField] private RoomPlayer roomPlayerPrefab;
        [SerializeField] private Transform playerList;
        [SerializeField] private MapListData mapList;

        private List<RoomPlayer> roomPlayerList;
        private RoomPlayer myRoomPlayer;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            mapDropdown.onValueChanged.AddListener(ChangeMap);
            teamDropdown.onValueChanged.AddListener(ChangeTeam);

            roomPlayerList = new List<RoomPlayer>();
            HideRoomMenu();
        }

        private void Start()
        {
            foreach (var map in mapList.MapList)
            {
                mapDropdown.AddOptions(new List<string>() { map.MapName });
            }
            teamDropdown.AddOptions(new List<string>() { "None", "Red", "Blue" });
        }

        private void ChangeTeam(int value)
        {
            myRoomPlayer.SetTeam((PunTeams.Team)value);
        }

        private void ChangeMap(int value)
        {
            photonView.RPC("RPCChangeMap", PhotonTargets.OthersBuffered, value);
        }

        [PunRPC]
        private void RPCChangeMap(int value)
        {
            mapDropdown.value = value;
        }

        public void ShowRoomMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideRoomMenu()
        {
            gameObject.SetActive(false);
        }

        public void RefreshRoom()
        {
            roomTitleText.text = PhotonNetwork.room.Name;
            startButton.enabled = PhotonNetwork.isMasterClient;
            mapDropdown.enabled = PhotonNetwork.isMasterClient;

            // Clean players from room
            foreach (RoomPlayer roomPlayer in roomPlayerList)
            {
                Destroy(roomPlayer.gameObject);
            }
            roomPlayerList.Clear();

            // Search and list all players in the room
            foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
            {
                var roomPlayer = PhotonNetwork.Instantiate("Menu/RoomPlayer", transform.position, Quaternion.identity, 0).GetComponent<RoomPlayer>();
                roomPlayer.transform.SetParent(playerList, false);
                roomPlayer.SetPlayer(photonPlayer);
                roomPlayer.SetTeam(photonPlayer.GetTeam());
                roomPlayerList.Add(roomPlayer);
                if(photonPlayer == PhotonNetwork.player)
                {
                    myRoomPlayer = roomPlayer;
                }
            }
        }

        private void StartGame()
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel(mapList.MapList[mapDropdown.value].SceneName);
            }
        }
    }
}
