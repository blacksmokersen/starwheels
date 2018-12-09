using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bolt;
using UdpKit;
using System;
using Utilities;
using TMPro;
using Multiplayer;
using Menu;
using UnityEngine.SceneManagement;

namespace Photon.Lobby
{
    /*
     * Logic code for the lobby
     *
     */
    public class LobbyManager : GlobalEventListener
    {
        static public LobbyManager Instance;

        public string MatchHost
        {
            get
            {
                if (BoltNetwork.isRunning)
                {
                    if (BoltNetwork.isServer)
                    {
                        return "<server>";
                    }

                    if (BoltNetwork.isClient)
                    {
                        return "<client>";
                    }
                }
                return "";
            }
        }

        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Lobby Configuration")]
        [SerializeField] private SceneField _lobbyScene;
        [SerializeField] private SceneField _gameScene;

        [Header("UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        [SerializeField] private float _prematchCountdown = 5.0f;

        [Space]
        [Header("UI Reference")]
        [SerializeField] private GameObject _mainGameMenu;
        [SerializeField] private MainMenu _mainMenuUI;
        [SerializeField] private TMP_Dropdown _mapDropdown;
        [SerializeField] private TMP_Dropdown _gamemodeDropdown;
        [SerializeField] private TMP_Dropdown _kartDropdown;
        [SerializeField] private TMP_Dropdown _abilityDropdown;

        [Header("Panels")]
        public LobbyTopPanel TopPanel;
        [SerializeField] private RectTransform _mainMenuPanel;
        [SerializeField] private RectTransform _lobbyPanel;
        [SerializeField] private LobbyInfoPanel _infoPanel;
        [SerializeField] private LobbyCountdownPanel _countdownPanel;
        [SerializeField] private RectTransform _currentPanel;

        [Header("Buttons")]
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _addPlayerButton;
        [SerializeField] private Button _startButton;
        public BackButtonDelegate BackDelegate;

        [Header("Info Texts")]
        [SerializeField] private Text _statusInfo;
        [SerializeField] private Text _hostInfo;

        private bool _countdownStarted = false;
        private string _matchName;

        // CORE

        private void Start()
        {
            Instance = this;
            _currentPanel = _mainMenuPanel;

            _backButton.gameObject.SetActive(true);
            _startButton.gameObject.SetActive(false);
            GetComponent<Canvas>().enabled = true;

            _kartDropdown.onValueChanged.AddListener(delegate { _playerSettings.KartIndex = _kartDropdown.value; });
            _abilityDropdown.onValueChanged.AddListener(delegate { _playerSettings.AbilityIndex = _abilityDropdown.value; });

            SetServerInfo("Offline", "None");
        }

        private void FixedUpdate()
        {
            if (BoltNetwork.isServer && !_countdownStarted)
            {
                VerifyReady();
            }
        }

        // BOLT CALLBACKS

        public override void BoltStartBegin()
        {
            BoltNetwork.RegisterTokenClass<RoomProtocolToken>();
            BoltNetwork.RegisterTokenClass<ServerAcceptToken>();
            BoltNetwork.RegisterTokenClass<ServerConnectToken>();
        }

        public override void BoltStartDone()
        {
            if (!BoltNetwork.isRunning) { return; }

            if (BoltNetwork.isServer)
            {
                RoomProtocolToken token = new RoomProtocolToken()
                {
                    RoomInfo = "My DATA",
                };

                BoltLog.Info("Starting Server");
                // Start Photon Room
                BoltNetwork.SetServerInfo(_matchName, token);

                // Setup Host
                _infoPanel.gameObject.SetActive(false);
                ChangeTo(_lobbyPanel);

                BackDelegate = Shutdown;
                SetServerInfo("Host", "");

                // Build Server Entity
                BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.PlayerInfo);
                entity.TakeControl();

            }
            else if (BoltNetwork.isClient)
            {
                BackDelegate = Shutdown;
                SetServerInfo("Client", "");
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            _matchName = "";
        }

        public override void SceneLoadLocalDone(string map)
        {
            BoltConsole.Write("Scene loaded : " + map, Color.yellow);
            Debug.Log("[Lobby] Scene loaded : " + map);

            try
            {
                if (_lobbyScene.SimpleSceneName == map)
                {
                    ChangeTo(_mainMenuPanel);
                    TopPanel.IsInGame = false;
                }
                else if (_gameScene.SimpleSceneName == map)
                {
                    ChangeTo(null);

                    BackDelegate = Shutdown;
                    TopPanel.IsInGame = true;
                    TopPanel.ToggleVisibility(false);
                }

            }
            catch (Exception e)
            {
                BoltConsole.Write(e.Message, Color.red);
                BoltConsole.Write(e.Source, Color.red);
                BoltConsole.Write(e.StackTrace, Color.red);
            }
        }

        #region Client Callbacks
        public override void OnEvent(LobbyCountdown evnt)
        {
            _countdownPanel.UIText.text = "Match Starting in " + evnt.Time;
            _countdownPanel.gameObject.SetActive(evnt.Time != 0);
        }

        public override void EntityReceived(BoltEntity entity)
        {
            BoltConsole.Write("EntityReceived");
        }

        public override void EntityAttached(BoltEntity entity)
        {
            BoltConsole.Write("EntityAttached");

            if (!entity.isControlled)
            {
                LobbyPhotonPlayer photonPlayer = entity.gameObject.GetComponent<LobbyPhotonPlayer>();

                if (photonPlayer != null)
                {
                    photonPlayer.SetupOtherPlayer();
                }
            }
        }

        public override void Connected(BoltConnection connection)
        {
            if (BoltNetwork.isClient)
            {
                BoltConsole.Write("Connected Client: " + connection, Color.blue);

                _infoPanel.gameObject.SetActive(false);
                ChangeTo(_lobbyPanel);

            }
            else if (BoltNetwork.isServer)
            {
                BoltConsole.Write("Connected Server: " + connection, Color.blue);

                BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.PlayerInfo);

                LobbyPhotonPlayer lobbyPlayer = entity.GetComponent<LobbyPhotonPlayer>();

                lobbyPlayer.Connection = connection;
                lobbyPlayer.SetPlayerID((int)connection.ConnectionId);

                connection.UserData = lobbyPlayer;
                connection.SetStreamBandwidth(1024 * 1024);

                entity.AssignControl(connection);
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            LobbyPhotonPlayer player = connection.GetLobbyPlayer();
            if (player != null)
            {
                BoltLog.Info("Disconnected");
                //player.RemovePlayer();
            }
        }

        public override void ConnectFailed(UdpEndPoint endpoint, IProtocolToken token)
        {
            // Do some stuff
        }
        #endregion

        // PUBLIC

        public void ChangeTo(RectTransform newPanel)
        {
            if (_currentPanel != null)
            {
                _currentPanel.gameObject.SetActive(false);
            }

            if (newPanel != null)
            {
                newPanel.gameObject.SetActive(true);
            }

            _currentPanel = newPanel;

            if (_currentPanel != _mainMenuPanel)
            {
                _backButton.gameObject.SetActive(true);
            }
            else
            {
                SetServerInfo("Offline", "None");
            }
        }

        public void DisplayIsConnecting()
        {
            var _this = this;
            _infoPanel.Display("Connecting...", "Cancel", () => { _this.BackDelegate(); });
        }

        public void SetServerInfo(string status, string host)
        {
            _statusInfo.text = status;
            _hostInfo.text = host;
        }

        public delegate void BackButtonDelegate();

        public void GoBackButton()
        {
            if (_currentPanel == _mainMenuPanel)
            {
                /*
                gameObject.SetActive(false);
                _mainMenuUI.UpdateMenu();
                _mainGameMenu.SetActive(true);
                */

                //TEMP lobby rework
                BoltLauncher.Shutdown();
                SceneManager.LoadScene("Menu");

            }
            else
            {
                /*
                BackDelegate();
                TopPanel.IsInGame = false;
                */
                //TEMP lobby rework
                BoltLauncher.Shutdown();
                SceneManager.LoadScene("Menu");

            }
        }

        public void StartClient()
        {
            BoltLauncher.StartClient();
        }

        public void Shutdown()
        {
            if (BoltNetwork.isConnected)
            {
                BoltLauncher.Shutdown();
            }
        }

        public void CreateMatch(string matchName, bool dedicated = false)
        {
            StartServer();
            _matchName = matchName;
        }

        public void SimpleBackCallback()
        {
            ChangeTo(_mainMenuPanel);
        }

        // PRIVATE

        private void VerifyReady()
        {
            if (!LobbyPlayerList.Ready) { return; }

            bool allReady = true;
            int readyCount = 0;

            foreach (LobbyPhotonPlayer player in LobbyPlayerList.Instance.AllPlayers)
            {
                allReady = allReady && player.IsReady;

                if (!allReady) { break; }

                readyCount++;
            }

            if (allReady)
            {
                _countdownStarted = true;
                _startButton.gameObject.SetActive(true);
            }
            else
            {
                _startButton.gameObject.SetActive(false);
            }
        }

        private void StartServer()
        {
            BoltLauncher.StartServer();
        }

        private void StartGame()
        {
            StartCoroutine(ServerCountdownCoroutine());
        }

        private IEnumerator ServerCountdownCoroutine()
        {
            float remainingTime = _prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            LobbyCountdown countdown;

            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;
                int newFloorTime = Mathf.FloorToInt(remainingTime);

                if (newFloorTime != floorTime)
                {
                    floorTime = newFloorTime;

                    countdown = LobbyCountdown.Create(GlobalTargets.Everyone);
                    countdown.Time = floorTime;
                    countdown.Send();
                }
            }

            countdown = LobbyCountdown.Create(GlobalTargets.Everyone);
            countdown.Time = 0;
            countdown.Send();

            var token = new RoomProtocolToken();
            token.PlayersCount = LobbyPlayerList.Instance.AllPlayers.Count;
            token.Gamemode = _gamemodeDropdown.options[_gamemodeDropdown.value].text;
            BoltNetwork.LoadScene(_mapDropdown.options[_mapDropdown.value].text, token);
        }
    }
}
