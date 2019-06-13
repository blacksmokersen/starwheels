using Bolt;
using UdpKit;

namespace SW.Matchmaking
{
    public class JoinToken : IProtocolToken
    {
        public string Nickname;

        public void Read(UdpPacket packet)
        {
            Nickname = packet.ReadString();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(Nickname);
        }
    }
}
