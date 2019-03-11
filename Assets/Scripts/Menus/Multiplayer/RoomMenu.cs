using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class RoomMenu : MonoBehaviour
    {
        [SerializeField] private MultiplayerMenu multiplayerMenu;

        [SerializeField] private Button leaveRoomButton;
        [SerializeField] private Button changeNicknameButton;
        [SerializeField] private Button switchTeamButton;
        [SerializeField] private Button startGameButton;

        [SerializeField] private Dropdown mapDropdown;
        // TODO: This has to be inside a custom dropdown script
        // When interactable = false, hide the arrow inside the custom script instead of here
        [SerializeField] private Image mapDropdownArrow;
        [SerializeField] private Dropdown abilityDropdown;

        [SerializeField] private Text roomNameText;
        [SerializeField] private Text playerCountText;

        [SerializeField] private GameObject panelPlayerList;
        [SerializeField] private GameObject panelRedTeam;
        [SerializeField] private GameObject panelBlueTeam;
        [SerializeField] private GameObject rowPlayerPrefab;

        [SerializeField] private Button backButton;

        // CORE

        private void Awake()
        {
            /*leaveRoomButton.onClick.AddListener(() => PhotonNetwork.LeaveRoom());
            changeNicknameButton.onClick.AddListener(
                () => FindObjectOfType<StringInput>().GetStringInput("Enter your name", ChangeLocalplayerName)
            );
            switchTeamButton.onClick.AddListener(SwitchTeam);
            startGameButton.onClick.AddListener(StartGame);

            mapDropdown.onValueChanged.AddListener(SetRoomMap);
            */
        }

        /*
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            CreateRowPlayer(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player player)
        {
            if (player.IsInactive) return;

            RemoveRowPlayer(player);
            UpdatePlayerCount();
        }

        public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
        {
            UpdatePlayerName(target);
            ChangePlayerTeam(target);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            UpdateMapName();
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            UpdateRoomHost();
        }

        public override void OnLeftRoom()
        {
            multiplayerMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
            backButton.interactable = true;
        }

        // PUBLIC

        public void Initialize()
        {
            UpdateRoomHost();
            UpdateRoomName();
            UpdatePlayerCount();
            UpdateMapName();

            // Create row player for all players already in the room
            foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                CreateRowPlayer(player);
            }
        }

        // PRIVATE

        private void UpdateRoomName()
        {
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        }

        private void UpdatePlayerCount()
        {
            playerCountText.text = "" + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        }

        private void UpdatePlayerName(Player player)
        {
            FindRowPlayer(player).SetName(player.NickName);
        }

        private void UpdateRoomHost()
        {
            bool isHost = PhotonNetwork.IsMasterClient;

            startGameButton.interactable = isHost;
            mapDropdown.interactable = isHost;
            mapDropdownArrow.enabled = isHost;
        }

        private void UpdateMapName()
        {
            mapDropdown.value = (int)PhotonNetwork.CurrentRoom.CustomProperties["map"];
        }

        private void SetRoomMap(int id)
        {
            Hashtable props = new Hashtable();
            props["map"] = id;

            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }

        private void CreateRowPlayer(Player player)
        {
            Instantiate(rowPlayerPrefab, GetTeamPanel(player.GetTeam()).transform).GetComponent<RowPlayer>().SetPlayer(player);
            UpdatePlayerCount();
        }

        private void RemoveRowPlayer(Player player)
        {
            Destroy(FindRowPlayer(player).gameObject);
        }

        private void ChangePlayerTeam(Player player)
        {
            RowPlayer rowPlayer = FindRowPlayer(player);
            PunTeams.Team newTeam = player.GetTeam();

            rowPlayer.SetTeam(newTeam);
            rowPlayer.transform.SetParent(GetTeamPanel(newTeam).transform);
        }

        private GameObject GetTeamPanel(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.red: return panelRedTeam;
                case PunTeams.Team.blue: return panelBlueTeam;
            }

            return null;
        }

        private RowPlayer FindRowPlayer(Player player)
        {
            var players = panelPlayerList.GetComponentsInChildren<RowPlayer>();

            foreach (RowPlayer p in players)
            {
                if (p.GetPlayerId() == player.ActorNumber) return p;
            }

            return null;
        }

        private void ChangeLocalplayerName(string newName)
        {
            PhotonNetwork.LocalPlayer.NickName = newName;
        }

        private void StartGame()
        {
            switch (mapDropdown.value)
            {
                case 0:
                    PhotonNetwork.LoadLevel(Constants.Scene.FortBlock);
                    break;
                case 1:
                    PhotonNetwork.LoadLevel(Constants.Scene.Pillars);
                    break;
            }
        }

        private void SwitchTeam()
        {
            var newTeam = PunTeams.Team.red;

            if (PhotonNetwork.LocalPlayer.GetTeam() == PunTeams.Team.red)
            {
                newTeam = PunTeams.Team.blue;
            }

            PhotonNetwork.LocalPlayer.SetTeam(newTeam);
        }
        */
    }
}
