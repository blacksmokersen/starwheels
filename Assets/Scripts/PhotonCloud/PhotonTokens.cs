using UdpKit;
using System;

namespace Photon
{
    public class RoomProtocolToken : Bolt.IProtocolToken
    {
        public String RoomInfo;
        public String Gamemode;
        public String RespawnMode;
        public int PlayersCount;
        public bool GameStarted = false;

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

    public class ServerAcceptToken : Bolt.IProtocolToken
    {
        public String Data;

        public void Read(UdpPacket packet)
        {
            Data = packet.ReadString();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(Data);
        }
    }

    public class ServerConnectToken : Bolt.IProtocolToken
    {
        public String Data;

        public void Read(UdpPacket packet)
        {
            Data = packet.ReadString();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(Data);
        }
    }
}
