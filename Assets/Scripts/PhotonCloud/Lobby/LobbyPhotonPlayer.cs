using UnityEngine;
using UnityEngine.UI;
using Multiplayer;
using Multiplayer.Teams;
using Bolt;

namespace Photon.Lobby
{
    public class LobbyPhotonPlayer : EntityEventListener<ILobbyPlayerInfoState>
    {
        [Header("Player Data")]
        public BoltConnection Connection;
        public bool IsReady
        {
            get { return state.Ready; }
        }
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Lobby")]
        [SerializeField] private string _playerName = "";
        [SerializeField] private Color _playerColor = TeamsColors.GetColorFromTeam(Team.None);
        [SerializeField] private bool _isReady = false;

        [Header("Player Row UI")]
        [SerializeField] private Button _colorButton;
        [SerializeField] private InputField _nameInput;
        [SerializeField] private Button _readyButton;
        [SerializeField] private Button _removePlayerButton;

        [Header("Icons")]
        [SerializeField] private GameObject _localIcon;
        [SerializeField] private GameObject _remoteIcon;

        // Colors
        private Color OddRowColor = new Color(250.0f / 255.0f, 250.0f / 255.0f, 250.0f / 255.0f, 1.0f);
        private Color EvenRowColor = new Color(180.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f, 1.0f);
        private Color JoinColor = new Color(255.0f / 255.0f, 0.0f, 101.0f / 255.0f, 1.0f);
        private Color NotReadyColor = new Color(34.0f / 255.0f, 44 / 255.0f, 55.0f / 255.0f, 1.0f);
        private Color ReadyColor = new Color(0.0f, 204.0f / 255.0f, 204.0f / 255.0f, 1.0f);
        private Color TransparentColor = new Color(0, 0, 0, 0);

        // CORE

        private void Awake()
        {
            _colorButton.GetComponent<Image>().color = TeamsColors.GetColorFromTeam(Team.None);
        }

        // BOLT

        public override void Attached()
        {
            state.AddCallback("Name", () =>
            {
                _nameInput.text = state.Name;
            });

            state.AddCallback("Color", () =>
            {
                OnColorChanged(state.Color);
            });

            state.AddCallback("Ready", () =>
            {
                OnClientReady(state.Ready);
            });

            if (entity.isOwner)
            {
                state.Color = _playerColor;
                state.Name = "Player #" + Random.Range(1, 100);

                if (BoltNetwork.isClient)
                {
                    _playerSettings.ConnectionID = (int)Connection.ConnectionId;
                }
            }
        }

        public override void ControlGained()
        {
            BoltConsole.Write("ControlGained", Color.blue);

            _readyButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            SetupPlayer();

            _playerSettings.ConnectionID = state.PlayerID;
        }

        public override void OnEvent(LobbyPlayerKick evnt)
        {
            BoltConsole.Write("Received Kick event", Color.yellow);
            BoltLauncher.Shutdown();
        }

        public override void SimulateController()
        {
            ILobbyCommandInput input = LobbyCommand.Create();

            input.Name = _playerName;
            input.Color = _playerColor;
            input.Ready = _isReady;

            entity.QueueInput(input);
        }

        public override void ExecuteCommand(Command command, bool resetState)
        {
            if (!entity.isOwner) { return; }

            if (!resetState && command.IsFirstExecution)
            {
                LobbyCommand lobbyCommand = command as LobbyCommand;

                state.Name = lobbyCommand.Input.Name;
                state.Color = lobbyCommand.Input.Color;
                state.Ready = lobbyCommand.Input.Ready;
            }
        }

        // PUBLIC

        public void OnPlayerListChanged(int idx)
        {
            GetComponent<Image>().color = (idx % 2 == 0) ? EvenRowColor : OddRowColor;
        }

        public void OnRemovePlayerClick()
        {
            if (BoltNetwork.isServer)
            {
                LobbyPlayerKick.Create(entity, EntityTargets.OnlyController).Send();
            }
        }

        public void SetPlayerID(int playerID)
        {
            state.PlayerID = playerID;
        }

        public void SetupOtherPlayer()
        {
            BoltConsole.Write("SetupOtherPlayer", Color.green);

            LobbyPlayerList.Instance.AddPlayer(this);

            _nameInput.interactable = false;

            _removePlayerButton.gameObject.SetActive(BoltNetwork.isServer);
            _removePlayerButton.interactable = BoltNetwork.isServer;

            ChangeReadyButtonColor(NotReadyColor);

            _readyButton.transform.GetChild(0).GetComponent<Text>().text = "...";
            _readyButton.interactable = false;

            OnClientReady(state.Ready);
        }

        public void RemovePlayer()
        {
            Debug.Log("Removing player");
            LobbyPlayerList.Instance.RemovePlayer(this);

            if (entity != null)
            {
                BoltNetwork.Destroy(entity.gameObject);
            }
        }

        // PRIVATE

        private void SetupPlayer()
        {
            BoltConsole.Write("SetupPlayer", Color.green);

            LobbyPlayerList.Instance.AddPlayer(this);

            _nameInput.interactable = true;
            _remoteIcon.gameObject.SetActive(false);
            _localIcon.gameObject.SetActive(true);

            _removePlayerButton.gameObject.SetActive(false);
            _removePlayerButton.interactable = false;

            ChangeReadyButtonColor(JoinColor);

            _readyButton.transform.GetChild(0).GetComponent<Text>().text = "JOIN";
            _readyButton.interactable = true;

            _colorButton.interactable = true;
            _nameInput.interactable = true;

            _nameInput.onEndEdit.RemoveAllListeners();
            _nameInput.onEndEdit.AddListener(OnNameChanged);

            _colorButton.onClick.RemoveAllListeners();
            _colorButton.onClick.AddListener(OnColorClicked);

            _readyButton.onClick.RemoveAllListeners();
            _readyButton.onClick.AddListener(OnReadyClicked);

            OnClientReady(state.Ready);
        }

        private void ChangeReadyButtonColor(Color c)
        {
            ColorBlock b = _readyButton.colors;
            b.normalColor = c;
            b.pressedColor = c;
            b.highlightedColor = c;
            b.disabledColor = c;
            _readyButton.colors = b;
        }

        private void OnColorChanged(Color newColor)
        {
            _playerColor = newColor;
            _colorButton.GetComponent<Image>().color = newColor;

            if (entity.isOwner && Connection)
            {
                Connection.UserData = newColor;
            }
        }

        private void OnColorClicked()
        {
            if(_playerColor == TeamsColors.GetColorFromTeam(Team.Blue))
                _playerColor = TeamsColors.GetColorFromTeam(Team.Red);
            else if (_playerColor == TeamsColors.GetColorFromTeam(Team.Red))
                _playerColor = TeamsColors.GetColorFromTeam(Team.Blue);
            else
                _playerColor = TeamsColors.GetColorFromTeam(Team.Blue);
            _playerSettings.Team = _playerColor;
        }

        private void OnReadyClicked()
        {
            _isReady = !_isReady;
        }

        private void OnNameChanged(string newName)
        {
            _playerName = newName;
            _playerSettings.Nickname = newName;
        }

        private void OnClientReady(bool readyState)
        {
            if (readyState)
            {
                ChangeReadyButtonColor(TransparentColor);

                Text textComponent = _readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = "READY";
                textComponent.color = ReadyColor;
                _readyButton.interactable = false;
                _colorButton.interactable = false;
                _nameInput.interactable = false;
            }
            else
            {
                ChangeReadyButtonColor(entity.isControlled ? JoinColor : NotReadyColor);

                Text textComponent = _readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = entity.isControlled ? "JOIN" : "...";
                textComponent.color = Color.white;
                _readyButton.interactable = entity.isControlled;
                _colorButton.interactable = entity.isControlled;
                _nameInput.interactable = entity.isControlled;
            }
        }
    }
}
