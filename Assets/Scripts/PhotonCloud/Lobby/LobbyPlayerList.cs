using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Lobby
{
    /*
     * List of players in the lobby
     *
     */
    public class LobbyPlayerList : MonoBehaviour
    {
        public static LobbyPlayerList Instance = null;

        [Header("UI Elements")]
        [SerializeField] private RectTransform _playerListContentTransform;
        [SerializeField] private GameObject _warningDirectPlayServer;
        [SerializeField] private Transform _addButtonRow;

        private VerticalLayoutGroup _layout;
        private List<LobbyPhotonPlayer> _players = new List<LobbyPhotonPlayer>();

        #region Properties
        public static bool Ready
        {
            get { return Instance != null; }
        }

        public List<LobbyPhotonPlayer> AllPlayers
        {
            get { return _players; }
        }

        public bool ServerIsPlaying
        {
            get { return ServerPlayer != null; }
        }

        public LobbyPhotonPlayer ServerPlayer
        {
            get;
            set;
        }

        public LobbyPhotonPlayer CreatePlayer()
        {
            if (!BoltNetwork.isClient) { return null; }

            return null;
        }
        #endregion

        // CORE

        private void OnEnable()
        {
            Instance = this;
            _players = new List<LobbyPhotonPlayer>();
            _layout = _playerListContentTransform.GetComponent<VerticalLayoutGroup>();
        }

        private void Update()
        {
            // Recompute the layout everyframe

            if (_layout)
            {
                _layout.childAlignment = Time.frameCount % 2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
            }
        }

        // PUBLIC

        public void DisplayDirectServerWarning(bool enabled)
        {
            if(_warningDirectPlayServer != null)
                _warningDirectPlayServer.SetActive(enabled);
        }

        public LobbyPhotonPlayer GetPlayer(BoltConnection connection)
        {
            foreach(var player in _players)
            {
                if (player.Connection == connection)
                    return player;
            }
            return null;
        }

        public void AddPlayer(LobbyPhotonPlayer player)
        {
            if (_players.Contains(player))
                return;

            _players.Add(player);

            player.transform.SetParent(_playerListContentTransform, false);
            _addButtonRow.transform.SetAsLastSibling();

            OnPlayerListModified();
        }

        public void RemovePlayer(LobbyPhotonPlayer player)
        {
            _players.Remove(player);

            OnPlayerListModified();
        }

        public void OnPlayerListModified()
        {
            int i = 0;
            foreach (LobbyPhotonPlayer p in _players)
            {
                p.OnPlayerListChanged(i);
                ++i;
            }
        }
    }
}
