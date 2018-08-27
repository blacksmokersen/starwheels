using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;
using ExitGames.Client.Photon;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : UnityEngine.MonoBehaviour, IPunCallbacks
{
    [SerializeField] private Button soloButton;
    [SerializeField] private Button multiButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject multiplayer;

    private Button[] buttons;
    private int currentIndex;
    private float oldInput;

    private void Awake()
    {
        buttons = new Button[4] { soloButton, multiButton, optionsButton, quitButton };

        soloButton.onClick.AddListener(Solo);
        multiButton.onClick.AddListener(Multi);
        quitButton.onClick.AddListener(Quit);
        Select(currentIndex);
    }

    private void Start()
    {
        PhotonNetwork.autoJoinLobby = true;
    }

    private void Update()
    {
        float ver = Input.GetAxis("Vertical");

        if (ver != oldInput)
        {
            Unselect(currentIndex);
            if (ver == -1) currentIndex++;
            else if (ver == 1) currentIndex--;
            if (currentIndex < 0) currentIndex = buttons.Length - 1;
            if (currentIndex >= buttons.Length) currentIndex = 0;
            Select(currentIndex);
            oldInput = ver;
        }

        if (Input.GetButtonDown("Fire"))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    private void Unselect(int index)
    {
        buttons[index].targetGraphic.color = buttons[index].colors.normalColor;
    }
    private void Select(int index)
    {
        buttons[index].targetGraphic.color = buttons[index].colors.highlightedColor;
    }

    private void Multi()
    {
        multiplayer.SetActive(true);
    }

    private void Solo()
    {
        Debug.Log("Launching Solo mode");
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.CreateRoom("Solo");
        SceneManager.LoadScene(1);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnConnectedToMaster() {}
    public void OnCreatedRoom() {}
    public void OnJoinedLobby() {}
    public void OnJoinedRoom() {}
    public void OnConnectedToPhoton() { }
    public void OnLeftRoom() {}
    public void OnMasterClientSwitched(PhotonPlayer newMasterClient) {}
    public void OnPhotonCreateRoomFailed(object[] codeAndMsg) {}
    public void OnPhotonJoinRoomFailed(object[] codeAndMsg) {}
    public void OnLeftLobby() {}
    public void OnFailedToConnectToPhoton(DisconnectCause cause) {}
    public void OnConnectionFail(DisconnectCause cause) {}
    public void OnDisconnectedFromPhoton() {}
    public void OnPhotonInstantiate(PhotonMessageInfo info) {}
    public void OnReceivedRoomListUpdate() {}
    public void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {}
    public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {}
    public void OnPhotonRandomJoinFailed(object[] codeAndMsg) {}
    public void OnPhotonMaxCccuReached() {}
    public void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {}
    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) {}
    public void OnUpdatedFriendList() {}
    public void OnCustomAuthenticationFailed(string debugMessage) {}
    public void OnCustomAuthenticationResponse(Dictionary<string, object> data) {}
    public void OnWebRpcResponse(OperationResponse response) {}
    public void OnOwnershipRequest(object[] viewAndPlayer) {}
    public void OnLobbyStatisticsUpdate() {}
    public void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer) {}
    public void OnOwnershipTransfered(object[] viewAndPlayers) {}
}
