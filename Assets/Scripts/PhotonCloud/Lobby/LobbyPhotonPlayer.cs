using Bolt;
using UnityEngine;
using UnityEngine.UI;
using Multiplayer;
using Multiplayer.Teams;

namespace Photon.Lobby
{
    public class LobbyPhotonPlayer : Bolt.EntityEventListener<ILobbyPlayerInfoState>
    {
        // Bolt
        public BoltConnection connection;

        public bool IsReady
        {
            get { return state.Ready; }
        }

        [SerializeField] private PlayerSettings _playerSettings;

        // Lobby
        public string playerName = "";
        public Color playerColor = TeamsColors.GetColorFromTeam(Team.Blue);
        public bool ready = false;

        public Button colorButton;
        public InputField nameInput;
        public Button readyButton;
        public Button waitingPlayerButton;
        public Button removePlayerButton;

        public GameObject localIcone;
        public GameObject remoteIcone;

        public Color OddRowColor = new Color(250.0f / 255.0f, 250.0f / 255.0f, 250.0f / 255.0f, 1.0f);
        public Color EvenRowColor = new Color(180.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f, 1.0f);

        static Color JoinColor = new Color(255.0f / 255.0f, 0.0f, 101.0f / 255.0f, 1.0f);
        static Color NotReadyColor = new Color(34.0f / 255.0f, 44 / 255.0f, 55.0f / 255.0f, 1.0f);
        static Color ReadyColor = new Color(0.0f, 204.0f / 255.0f, 204.0f / 255.0f, 1.0f);
        static Color TransparentColor = new Color(0, 0, 0, 0);

        public static LobbyPhotonPlayer localPlayer;

        public override void Attached()
        {
            if (entity.isOwner)
            {
                state.Color = playerColor;
                state.Name = "Player #" + Random.Range(1, 100);

                if (BoltNetwork.isClient)
                {
                    _playerSettings.ConnectionID = (int)connection.ConnectionId;
                }
            }

            state.AddCallback("Name", () =>
            {
                //OnNameChanged(state.Name);
                nameInput.text = state.Name;
            });

            state.AddCallback("Color", () =>
            {
                OnColorChanged(state.Color);
                colorButton.GetComponent<Image>().color = state.Color;
            });

            state.AddCallback("Ready", () =>
            {
                OnClientReady(state.Ready);
            });
        }

        public override void ControlGained()
        {
            BoltConsole.Write("ControlGained", Color.blue);

            readyButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            SetupPlayer();

            _playerSettings.ConnectionID = state.PlayerID;
        }

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

        public override void OnEvent(LobbyPlayerKick evnt)
        {
            BoltConsole.Write("Received Kick event", Color.yellow);
            BoltLauncher.Shutdown();
        }

        public override void SimulateController()
        {
            ILobbyCommandInput input = LobbyCommand.Create();

            input.Name = playerName;
            input.Color = playerColor;
            input.Ready = ready;

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

        // Commands

        public void SetPlayerID(int playerID)
        {
            state.PlayerID = playerID;
        }

        public void SetupOtherPlayer()
        {
            BoltConsole.Write("SetupOtherPlayer", Color.green);

            LobbyPlayerList._instance.AddPlayer(this);

            nameInput.interactable = false;

            removePlayerButton.gameObject.SetActive(BoltNetwork.isServer);
            removePlayerButton.interactable = BoltNetwork.isServer;

            ChangeReadyButtonColor(NotReadyColor);

            readyButton.transform.GetChild(0).GetComponent<Text>().text = "...";
            readyButton.interactable = false;

            OnClientReady(state.Ready);
        }

        public void SetupPlayer()
        {
            BoltConsole.Write("SetupPlayer", Color.green);

            LobbyPlayerList._instance.AddPlayer(this);
            localPlayer = this;

            nameInput.interactable = true;
            remoteIcone.gameObject.SetActive(false);
            localIcone.gameObject.SetActive(true);

            removePlayerButton.gameObject.SetActive(false);
            removePlayerButton.interactable = false;

            ChangeReadyButtonColor(JoinColor);

            readyButton.transform.GetChild(0).GetComponent<Text>().text = "JOIN";
            readyButton.interactable = true;

            //we switch from simple name display to name input
            colorButton.interactable = true;
            nameInput.interactable = true;

            nameInput.onEndEdit.RemoveAllListeners();
            nameInput.onEndEdit.AddListener(OnNameChanged);

            colorButton.onClick.RemoveAllListeners();
            colorButton.onClick.AddListener(OnColorClicked);

            readyButton.onClick.RemoveAllListeners();
            readyButton.onClick.AddListener(OnReadyClicked);

            OnClientReady(state.Ready);
        }

        public void RemovePlayer()
        {
            Debug.Log("Removing player");
            LobbyPlayerList._instance.RemovePlayer(this);

            if (entity != null)
            {
                BoltNetwork.Destroy(entity.gameObject);
            }
        }

        // UI

        void ChangeReadyButtonColor(Color c)
        {
            ColorBlock b = readyButton.colors;
            b.normalColor = c;
            b.pressedColor = c;
            b.highlightedColor = c;
            b.disabledColor = c;
            readyButton.colors = b;
        }

        public void OnColorChanged(Color newColor)
        {
            playerColor = newColor;
            colorButton.GetComponent<Image>().color = newColor;
        }

        public void OnColorClicked()
        {
            if(playerColor == TeamsColors.GetColorFromTeam(Team.Blue))
                playerColor = TeamsColors.GetColorFromTeam(Team.Red);
            else if (playerColor == TeamsColors.GetColorFromTeam(Team.Red))
                playerColor = TeamsColors.GetColorFromTeam(Team.Blue);
            else
                playerColor = TeamsColors.GetColorFromTeam(Team.Blue);
            _playerSettings.Team = playerColor;
        }

        public void OnReadyClicked()
        {
            ready = !ready;
        }

        public void OnNameChanged(string newName)
        {
            playerName = newName;
            _playerSettings.Nickname = newName;
        }

        public void OnClientReady(bool readyState)
        {
            if (readyState)
            {
                ChangeReadyButtonColor(TransparentColor);

                Text textComponent = readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = "READY";
                textComponent.color = ReadyColor;
                readyButton.interactable = false;
                colorButton.interactable = false;
                nameInput.interactable = false;
            }
            else
            {
                ChangeReadyButtonColor(entity.isControlled ? JoinColor : NotReadyColor);

                Text textComponent = readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = entity.isControlled ? "JOIN" : "...";
                textComponent.color = Color.white;
                readyButton.interactable = entity.isControlled;
                colorButton.interactable = entity.isControlled;
                nameInput.interactable = entity.isControlled;
            }
        }
    }
}
