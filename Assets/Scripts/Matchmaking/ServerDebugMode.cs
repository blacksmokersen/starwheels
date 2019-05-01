using UnityEngine;
using UnityEngine.UI;

namespace SW.Matchmaking
{
    public class ServerDebugMode : MonoBehaviour
    {
        [SerializeField]private GameObject _text;
        [SerializeField]private GameObject _serverNameMatchmakingPanel;
        [SerializeField]private GameObject _serverNameQuickMatchPanel;
        [SerializeField]private GameObject[] _TogglesQuickMatch;
        [SerializeField]private LobbyMaker _lobyMaker;
        [SerializeField]private LobbyJoiner _lobyJoiner;

        [HideInInspector] public string ServerNameHost;
        [HideInInspector] public string ServerNameClient;

        //CORE

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                _lobyMaker.DebugModEnabled = !_lobyMaker.DebugModEnabled;
                _lobyJoiner.DebugModEnabled = !_lobyJoiner.DebugModEnabled;
                _text.SetActive(_lobyMaker.DebugModEnabled);
                _serverNameMatchmakingPanel.SetActive(_lobyMaker.DebugModEnabled);
                foreach(GameObject gameObject in _TogglesQuickMatch)
                {
                    gameObject.SetActive(!_lobyMaker.DebugModEnabled);
                }
                _serverNameQuickMatchPanel.SetActive(_lobyMaker.DebugModEnabled);
            }
        }

        //PUBLIC

        public string GetHostServerName()
        {
            ServerNameHost = _serverNameMatchmakingPanel.GetComponentInChildren<InputField>().text;
            return ServerNameHost;
        }

        public string GetClientServerName()
        {
            ServerNameClient = _serverNameQuickMatchPanel.GetComponentInChildren<InputField>().text;
            return ServerNameClient;
        }
    }
}

