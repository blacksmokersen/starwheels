using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Multiplayer
{
    public class Server : MonoBehaviour, IPunCallbacks
    {
        public string GameVersion;

        [SerializeField] private RoomMenu roomMenu;
        [SerializeField] private LobbyMenu lobbyMenu;

        private void Awake()
        {
            PhotonNetwork.offlineMode = false;
            PhotonNetwork.autoJoinLobby = true;
            PhotonNetwork.automaticallySyncScene  = true;
            PhotonNetwork.ConnectUsingSettings(GameVersion);
        }

        public void OnConnectedToPhoton()
        {
            Debug.Log("OnConnectedToPhoton");
            lobbyMenu.ShowLobbyMenu();
        }

        public void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            lobbyMenu.RefreshLobby();
        }

        public void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
        }

        public void OnReceivedRoomListUpdate()
        {
            Debug.Log("OnReceivedRoomListUpdate");
            lobbyMenu.RefreshLobby();
        }

        public void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            lobbyMenu.HideLobbyMenu();
            roomMenu.ShowRoomMenu();
            roomMenu.RefreshRoom();
        }

        public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("OnPhotonPlayerConnected: " + newPlayer);
            roomMenu.RefreshRoom();
        }

        public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("OnPhotonPlayerDisconnected: " + otherPlayer);
            roomMenu.RefreshRoom();
        }

        public void OnLeftRoom()
        {
            Debug.Log("OnLeftRoom");
        }

        public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
        {
            Debug.Log("OnMasterClientSwitched: " + newMasterClient);
        }

        public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonCreateRoomFailed");
        }

        public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonJoinRoomFailed");
        }

        public void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom");
        }

        public void OnLeftLobby()
        {
            Debug.Log("OnLeftLobby");
        }

        public void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            Debug.Log("OnFailedToConnectToPhoton: " + cause);
        }

        public void OnConnectionFail(DisconnectCause cause)
        {
            Debug.Log("OnConnectionFail: " + cause);
        }

        public void OnDisconnectedFromPhoton()
        {
            Debug.Log("OnDisconnectedFromPhoton");
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            Debug.Log("OnPhotonInstantiate: " + info);
        }

        public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonRandomJoinFailed");
        }

        public void OnPhotonMaxCccuReached()
        {
            Debug.Log("OnPhotonMaxCccuReached");
        }

        public void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            Debug.Log("OnPhotonCustomRoomPropertiesChanged");
        }

        public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
        {
            Debug.Log("OnPhotonPlayerPropertiesChanged");
        }

        public void OnUpdatedFriendList()
        {
            Debug.Log("OnUpdatedFriendList");
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("OnCustomAuthenticationFailed: " + debugMessage);
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            Debug.Log("OnCustomAuthenticationResponse");
        }

        public void OnWebRpcResponse(OperationResponse response)
        {
            Debug.Log("OnWebRpcResponse: " + response);
        }

        public void OnOwnershipRequest(object[] viewAndPlayer)
        {
            Debug.Log("OnOwnershipRequest");
        }

        public void OnLobbyStatisticsUpdate()
        {
            Debug.Log("OnLobbyStatisticsUpdate");
        }

        public void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
        {
            Debug.Log("OnPhotonPlayerActivityChanged: " + otherPlayer);
        }

        public void OnOwnershipTransfered(object[] viewAndPlayers)
        {
            Debug.Log("OnOwnershipTransfered");
        }
    }
}
