using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class RoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private MultiplayerMenu multiplayerMenu;

        [SerializeField] private Button leaveRoomButton;
        [SerializeField] private Button changeNicknameButton;
        [SerializeField] private Button switchTeamButton;
        [SerializeField] private Button startGameButton;

        [SerializeField] private Dropdown mapChoiceDropdown;
        [SerializeField] private Dropdown abilityChoiceDropdown;

        [SerializeField] private Text roomNameText;
        [SerializeField] private Text playerCountText;

        [SerializeField] private GameObject panelPlayerList;
        [SerializeField] private GameObject rowPlayerPrefab;

        [SerializeField] private Button backButton;

        // CORE

        private void Awake()
        {
            leaveRoomButton.onClick.AddListener(() => PhotonNetwork.LeaveRoom());
            changeNicknameButton.onClick.AddListener(
                () => FindObjectOfType<StringInput>().GetStringInput("New nickname", ChangeNickname)
            );
            switchTeamButton.onClick.AddListener(SwitchTeam);
            startGameButton.onClick.AddListener(StartGame);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            CreateRowPlayer(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (otherPlayer.IsInactive) return;

            Destroy(FindRowPlayer(otherPlayer).gameObject);
            UpdatePlayerCount();
        }

        public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
        {
            FindRowPlayer(target).SetNickName(target.NickName);
            FindRowPlayer(target).SetTeam(target.GetTeam());
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

        public void Refresh()
        {
            UpdateRoomName();
            UpdatePlayerCount();
            UpdateRoomHost();
            UpdatePlayerList();
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

        private void UpdateRoomHost()
        {
            startGameButton.interactable = PhotonNetwork.IsMasterClient;
        }

        private void UpdatePlayerList()
        {
            ClearPlayerList();

            foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                CreateRowPlayer(player);
            }
        }

        private void ClearPlayerList()
        {
            for (int i = 1; i < panelPlayerList.transform.childCount; ++i)
            {
                Destroy(panelPlayerList.transform.GetChild(i).gameObject);
            }
        }

        private void CreateRowPlayer(Player player)
        {
            var rowPlayer = Instantiate(rowPlayerPrefab, panelPlayerList.transform).GetComponent<RowPlayer>();
            rowPlayer.SetPlayer(player);
            UpdatePlayerCount();
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

        private void ChangeNickname(string newName)
        {
            PhotonNetwork.LocalPlayer.NickName = newName;
        }

        private void StartGame()
        {
            switch (mapChoiceDropdown.value)
            {
                case 1:
                    PhotonNetwork.LoadLevel(Constants.Scene.FortBlock);
                    break;
                case 2:
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
    }
}
