using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

namespace Multiplayer
{
    public class Room : PunBehaviour
    {
        [SerializeField] private Text _roomTitleText;
        [SerializeField] private RoomPlayer _roomPlayerPrefab;
        [SerializeField] private Button _startButton;
        [SerializeField] private Transform _playerList;
        [SerializeField] private Dropdown _mapDropdown;
        [SerializeField] private MapListData _mapList;

        private List<RoomPlayer> _roomPlayerList;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartGame);
            _roomPlayerList = new List<RoomPlayer>();

            _mapDropdown.onValueChanged.AddListener(ChangeMap);

            Hide();
        }

        private void ChangeMap(int value)
        {
            photonView.RPC("RPCChangeMap", PhotonTargets.OthersBuffered, value);
        }

        [PunRPC]
        private void RPCChangeMap(int value)
        {
            _mapDropdown.value = value;
        }

        private void Start()
        {
            foreach (var map in _mapList.MapList)
            {
                _mapDropdown.AddOptions(new List<string>() { map.MapName });
            }
        }
        
        public void Show() { gameObject.SetActive(true); }
        public void Hide() { gameObject.SetActive(false); }

        public void RefreshRoom()
        {
            _roomTitleText.text = PhotonNetwork.room.Name;
            _startButton.enabled = PhotonNetwork.isMasterClient;
            _mapDropdown.enabled = PhotonNetwork.isMasterClient;

            foreach (RoomPlayer item in _roomPlayerList)
            {
                Destroy(item.gameObject);
            }

            _roomPlayerList.Clear();
            
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                RoomPlayer item = Instantiate(_roomPlayerPrefab);
                item.transform.SetParent(_playerList, false);
                item.SetPlayer(player);
                _roomPlayerList.Add(item);
            }
        }

        private void StartGame()
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel(_mapList.MapList[_mapDropdown.value].SceneName);
            }
        }
    }
}
