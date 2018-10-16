using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UdpKit;

public class PlayerInfoToken : IProtocolToken
{
    public int Team;
    public string Nickname;

    public void Read(UdpPacket packet)
    {
        Team = packet.ReadInt();
        Nickname = packet.ReadString();
    }

    public void Write(UdpPacket packet)
    {
        packet.WriteInt(Team);
        packet.WriteString(Nickname);
    }
}
