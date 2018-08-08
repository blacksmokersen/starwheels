using System;
using System.Reflection;
using Photon;

namespace MyExtensions
{
    public class Functions
    {
        public static float RemapValue(float actualMin, float actualMax, float targetMin, float targetMax, float val)
        {
            return targetMin + (targetMax - targetMin) * ((val - actualMin) / (actualMax - actualMin));
        }
    }

    public static class Extensions
    {
        public static void ExecuteRPC(this PhotonView pun, object obj, PhotonTargets targets, string methodName, params object[] parameters)
        {
            if (PhotonNetwork.connected)
            {
                pun.photonView.RPC(methodName, targets, parameters);
            }
            else
            {
                Type thisType = pun.GetType();
                MethodInfo theMethod = thisType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);
                theMethod.Invoke(pun, parameters);
            }            
        }
    }
}
