using UnityEngine;
using Kart;

public class BaseKartComponent : MonoBehaviour
{
    protected KartEvents kartEvents;
    protected KartActions kartActions;
    protected void Awake()
    {
        kartEvents = GetComponentInParent<KartEvents>();
        kartActions = GetComponentInParent<KartActions>();
    }
}
