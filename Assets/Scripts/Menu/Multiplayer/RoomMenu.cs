using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomMenu : MonoBehaviourPunCallbacks {

    [SerializeField] private MultiplayerMenu multiplayerMenu;

    [SerializeField] private Button leaveRoomButton;
    [SerializeField] private Button changeNicknameButton;
    [SerializeField] private Button switchTeamButton;
    [SerializeField] private Button startGameButton;

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
        Destroy(FindRowPlayer(otherPlayer).gameObject);
        UpdatePlayerCount();
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        foreach (var change in changedProps)
        {
            Debug.Log("" + target.NickName + " has changed its '" + change.Key + "' to '" + change.Value + "'.");
        }
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

        if (PhotonNetwork.LocalPlayer.NickName != (string)PhotonNetwork.CurrentRoom.CustomProperties["owner"])
        {
            startGameButton.interactable = false;
        }

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

    private void UpdatePlayerList()
    {
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            CreateRowPlayer(player);
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
    }

    private void SwitchTeam()
    {
        var newTeam = PunTeams.Team.red;

        if (PhotonNetwork.LocalPlayer.GetTeam() == PunTeams.Team.red)
        {
            newTeam = PunTeams.Team.blue;
        }

        var parameters = new object[] { PhotonNetwork.LocalPlayer, newTeam };
        //photonView.RPC("ChangePlayerTeam", RpcTarget.AllBuffered, parameters);
    }

    [PunRPC]
    private void ChangePlayerTeam(Player player, PunTeams.Team newTeam)
    {
        FindRowPlayer(player).SetTeam(newTeam);
    }
}
