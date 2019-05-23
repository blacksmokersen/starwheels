using Bolt;
using UdpKit;

namespace Multiplayer
{
    public class RoomProtocolToken : IProtocolToken
    {
        public string RoomInfo;
        public string Gamemode;
        public int PlayersCount;

        public void Read(UdpPacket packet)
        {
            RoomInfo = packet.ReadString();
            Gamemode = packet.ReadString();
            PlayersCount = packet.ReadInt();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(RoomInfo);
            packet.WriteString(Gamemode);
            packet.WriteInt(PlayersCount);
        }
    }
}
