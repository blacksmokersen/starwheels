using UdpKit;
using System;

namespace Photon
{
    public class RoomProtocolToken : Bolt.IProtocolToken
    {
        public String ArbitraryData;

        private String _password;

        public void Read(UdpPacket packet)
        {
            ArbitraryData = packet.ReadString();
            _password = packet.ReadString();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(ArbitraryData);
            packet.WriteString(_password);
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
