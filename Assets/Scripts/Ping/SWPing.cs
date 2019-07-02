public static class SWPing
{
    public static int GetPingForPlayer(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            foreach (var client in BoltNetwork.Clients)
            {
                if ((int)client.ConnectionId == playerID)
                {
                    return (int)(client.PingNetwork * 1000);
                }
            }
        }
        return -1;
    }

    public static int GetPingAliasedForPlayer(int playerID)
    {
        if (BoltNetwork.IsServer)
        {
            foreach (var client in BoltNetwork.Clients)
            {
                if ((int)client.ConnectionId == playerID)
                {
                    return (int)(client.PingAliased * 1000);
                }
            }
        }
        return -1;
    }

    public static int GetServerPing()
    {
        if (BoltNetwork.IsClient)
        {
            return (int) (BoltNetwork.Server.PingNetwork * 1000);
        }
        else
        {
            return 0;
        }
    }

    public static int GetServerPingAliased()
    {
        if (BoltNetwork.IsClient)
        {
            return (int)(BoltNetwork.Server.PingAliased * 1000);
        }
        else
        {
            return 0;
        }
    }
}
