using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

namespace Multiplayer
{
    public class Room : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Text _roomTitleText;
        [SerializeField] private RoomPlayer _roomPlayerPrefab;
        [SerializeField] private Button _startButton;
        [SerializeField] private Transform _playerList;

        private List<RoomPlayer> _roomPlayerList;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartGame);
            _roomPlayerList = new List<RoomPlayer>();

            Hide();
        }
        
        public void Show() { gameObject.SetActive(true); }
        public void Hide() { gameObject.SetActive(false); }

        public void RefreshRoom()
        {
            _roomTitleText.text = PhotonNetwork.room.Name;
            _startButton.enabled = PhotonNetwork.isMasterClient;

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
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}
