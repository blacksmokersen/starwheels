using System;
using UdpKit;
using UnityEngine;

namespace SW.Matchmaking
{
    [CreateAssetMenu(menuName ="Matchmaking/Session Data")]
    public class SessionData : ScriptableObject
    {
        public UdpSession MySession;
        public Guid BoltSessionGuid;
    }
}
