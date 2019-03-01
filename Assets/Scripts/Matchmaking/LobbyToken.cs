using Bolt;
using UdpKit;

namespace SW.Matchmaking
{
    public class LobbyToken : IProtocolToken
    {
        public string ServerName;
        public string GameMode;
        public string MapName;
        public bool Public;
        public int MaxPlayers;

        public void Read(UdpPacket packet)
        {
            ServerName = packet.ReadString();
            GameMode = packet.ReadString();
            MapName = packet.ReadString();
            Public = packet.ReadBool();
            MaxPlayers = packet.ReadInt();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(ServerName);
            packet.WriteString(GameMode);
            packet.WriteString(MapName);
            packet.WriteBool(Public);
            packet.WriteInt(MaxPlayers);
        }

        public LobbyToken BuildData(LobbyData data)
        {
            ServerName = data.ServerName;
            GameMode = data.ChosenGamemode;
            MapName = data.ChosenMapName;
            Public = data.Public;
            MaxPlayers = data.MaxPlayers;
            return this;
        }
    }
}
