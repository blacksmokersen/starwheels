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
}
