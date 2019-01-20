using UnityEngine;

[CreateAssetMenu(menuName ="Common Settings/Size Increase")]
public class SizeIncreaseSettings : ScriptableObject
{
    public float SecondsBeforeFullSize;
    public Vector3 TargetSize;
    public Vector3 StartSize;
}
