using Bolt;
using UdpKit;

namespace SW.Matchmaking
{
    public class LobbyToken : IProtocolToken
    {
        public string ServerName;
        public string Version;
        public string GameMode;
        public string MapName;
        public bool Public;
        public bool GameStarted;
        public bool CanBeJoined;
        public int MaxPlayers;

        public void Read(UdpPacket packet)
        {
            ServerName = packet.ReadString();
            Version = packet.ReadString();
            GameMode = packet.ReadString();
            MapName = packet.ReadString();
            Public = packet.ReadBool();
            GameStarted = packet.ReadBool();
            CanBeJoined = packet.ReadBool();
            MaxPlayers = packet.ReadInt();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(ServerName);
            packet.WriteString(Version);
            packet.WriteString(GameMode);
            packet.WriteString(MapName);
            packet.WriteBool(Public);
            packet.WriteBool(GameStarted);
            packet.WriteBool(CanBeJoined);
            packet.WriteInt(MaxPlayers);
        }

        public LobbyToken BuildData(LobbyData data)
        {
            ServerName = data.ServerName;
            Version = data.Version;
            GameMode = data.ChosenGamemode;
            MapName = data.ChosenMapName;
            Public = data.Public;
            GameStarted = data.GameStarted;
            CanBeJoined = data.CanBeJoined;
            MaxPlayers = data.MaxPlayers;
            return this;
        }
    }
}
