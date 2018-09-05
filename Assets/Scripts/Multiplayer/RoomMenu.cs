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
            HideRoomMenu();
        }

        private void Start()
        {
            InitializeDropdowns();
            InstantiatePlayerPrefab();
            if (PhotonNetwork.isMasterClient)
            {
                MasterInitialization();
            }
        }
        private void MasterInitialization()
        {
            roomTitleText.text = PhotonNetwork.room.Name;
            startButton.enabled = true;
            mapDropdown.enabled = true;
        }

        private void InitializeDropdowns()
        {
            foreach (var map in mapList.MapList)
            {
                mapDropdown.AddOptions(new List<string>() { map.MapName });
            }
            teamDropdown.AddOptions(new List<string>() { "None", "Red", "Blue" });
        }

        private void InstantiatePlayerPrefab()
        {
            var playerPrefab = PhotonNetwork.Instantiate("Menu/RoomPlayer", playerList.transform.position, Quaternion.identity, 0);
            var roomPlayer = playerPrefab.GetComponent<RoomPlayer>();
            roomPlayer.SetTeam(PhotonNetwork.player.GetTeam());
            myRoomPlayer = roomPlayer;
        }

        private void StartGame()
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel(mapList.MapList[mapDropdown.value].SceneName);
            }
        }

        private void ChangeTeam(int value)
        {
            PhotonNetwork.player.SetTeam((PunTeams.Team)value);
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
    }
}
