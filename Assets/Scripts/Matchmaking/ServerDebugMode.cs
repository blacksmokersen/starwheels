using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SW.Matchmaking
{
    public class ServerDebugMode : MonoBehaviour
    {

        [Header("DebugMode")]
        [SerializeField] private bool _isDebugModeActivated;

        [Header("References")]
        [SerializeField] private GameObject _text;
        [SerializeField] private GameObject _serverNameMatchmakingPanel;
        [SerializeField] private GameObject _serverNameQuickMatchPanel;
        [SerializeField] private LobbyMaker _lobyMaker;
        [SerializeField] private LobbyJoiner _lobyJoiner;

        [HideInInspector] public string ServerNameHost;
        [HideInInspector] public string ServerNameClient;

        //CORE

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
                DebugModeActivator();
        }

        private void Start()
        {
            if (_isDebugModeActivated)
                DebugModeActivator();
        }

        //PUBLIC

        public void DebugModeActivator()
        {
            _lobyMaker.DebugModEnabled = !_lobyMaker.DebugModEnabled;
            _lobyJoiner.DebugModEnabled = !_lobyJoiner.DebugModEnabled;
            _text.SetActive(_lobyMaker.DebugModEnabled);
            _serverNameMatchmakingPanel.SetActive(_lobyMaker.DebugModEnabled);
            _serverNameQuickMatchPanel.SetActive(_lobyMaker.DebugModEnabled);
        }

        public string GetHostServerName()
        {
            ServerNameHost = _serverNameMatchmakingPanel.GetComponentInChildren<TMP_InputField>().text;
            return ServerNameHost;
        }

        public string GetClientServerName()
        {
            ServerNameClient = _serverNameQuickMatchPanel.GetComponentInChildren<TMP_InputField>().text;
            return ServerNameClient;
        }
    }
}

