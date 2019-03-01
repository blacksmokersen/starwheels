using System;
using SW.Matchmaking;
using UnityEngine;

public static class SWMatchmaking
{
    public static void CreateLobby()
    {
        if (!BoltNetwork.IsRunning && !BoltNetwork.IsServer)
        {
            BoltLauncher.StartServer();
            Debug.Log("Starting Bolt as server...");
        }
        else
        {
            Debug.LogWarning("Can't create a lobby if you are already a server.");
        }
    }

    public static void SetLobbyData(LobbyData lobbyData)
    {
        if (BoltNetwork.IsRunning && BoltNetwork.IsServer)
        {
            LobbyToken token = new LobbyToken().BuildData(lobbyData);
            BoltNetwork.SetServerInfo(lobbyData.ServerName, token);
            Debug.Log("Lobby data set.");
        }
        else
        {
            Debug.LogWarning("Can't set data if Bolt is not running.");
        }
    }

    public static LobbyToken GetLobbyToken(Guid lobbyID)
    {
        return (LobbyToken)BoltNetwork.SessionList[lobbyID].GetProtocolToken();
    }

    public static int GetCurrentLobbyPlayerCount()
    {
        int count = 0;
        foreach (var connection in BoltNetwork.Connections)
        {
            count++;
        }
        return count;
    }

    public static void JoinRandomLobby()
    {
        System.Random rnd = new System.Random();
        var randomSessionNumber = rnd.Next(BoltNetwork.SessionList.Count);
        var count = 0;
        foreach (var session in BoltNetwork.SessionList)
        {
            if (count == randomSessionNumber)
            {
                BoltNetwork.Connect(session.Value);
            }
            else
            {
                count++;
            }
        }
    }

    public static void JoinLobby(Guid id)
    {
        BoltNetwork.Connect(BoltNetwork.SessionList[id]);
    }

    public static void JoinRandomGame()
    {

    }
}

