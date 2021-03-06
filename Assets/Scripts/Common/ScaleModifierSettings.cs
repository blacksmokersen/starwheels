﻿using UnityEngine;

[CreateAssetMenu(menuName ="Common Settings/Size Increase")]
public class ScaleModifierSettings : ScriptableObject
{
    public bool StartIncreaseOnAwake = true;
    public float SecondsBeforeFullSize;
    public Vector3 TargetSize;
    public Vector3 StartSize;
}
