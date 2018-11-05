using System;
using UnityEngine;
using UdpKit;

namespace Photon.Lobby
{
    /*
     * UI elements and logic of the server list
     */
    public class LobbyServerList : Bolt.GlobalEventListener
    {
        [Header("Server list")]
        [SerializeField] private LobbyManager _lobbyManager;
        [SerializeField] private RectTransform _serverListRect;
        [SerializeField] private GameObject _serverEntryPrefab;
        [SerializeField] private GameObject _noServerFound;

        private int _currentPage = 0;

        static Color OddServerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f); // White
        static Color EvenServerColor = new Color(.94f, .94f, .94f, 1.0f); // Slightly gray

        // CORE

        private new void OnEnable()
        {
            base.OnEnable();

            _currentPage = 0;

            foreach (Transform t in _serverListRect)
            {
                Destroy(t.gameObject);
            }

            _noServerFound.SetActive(false);

            RequestPage(0);
        }

        // PUBLIC

        public override void SessionListUpdated(Map<Guid, UdpSession> matches)
        {
            if (matches.Count == 0)
            {
                _noServerFound.SetActive(true);
                return;
            }

            _noServerFound.SetActive(false);
            foreach (Transform t in _serverListRect)
            {
                Destroy(t.gameObject);
            }

            int serverCount = 0;
            foreach (var pair in matches)
            {
                UdpSession udpSession = pair.Value;

                GameObject serverEntry = Instantiate(_serverEntryPrefab) as GameObject;

                serverEntry.GetComponent<LobbyServerEntry>().Populate(udpSession, _lobbyManager, (serverCount % 2 == 0) ? OddServerColor : EvenServerColor);
                serverEntry.transform.SetParent(_serverListRect, false);

                ++serverCount;
            }
        }

        public void ChangePage(int dir)
        {
            int newPage = Mathf.Max(0, _currentPage + dir);

            if (_noServerFound.activeSelf)
                newPage = 0;

            RequestPage(newPage);
        }

        // PRIVATE

        private void RequestPage(int page)
        {
            _currentPage = page;
        }
    }
}
