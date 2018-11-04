using UnityEngine;
using Bolt.a2s;

public class A2SExample : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        BoltConsole.Write("Spawn Player on map " + map, Color.yellow);

        if (BoltNetwork.isServer)
        {
            A2SManager.SetPlayerInfo(null, "Photon Server");

            A2SManager.UpdateServerInfo(
                gameName: "Bolt Simple Tutorial",
                serverName: "Photon Bolt Server",
                map: map,
                version: "1.0",
                serverType: ServerType.Listen,
                visibility: Visibility.PUBLIC
            );
        }
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        if (BoltNetwork.isServer)
        {
            A2SManager.SetPlayerInfo(connection, "Conn: " + connection.ConnectionId.ToString());
        }
    }
}
