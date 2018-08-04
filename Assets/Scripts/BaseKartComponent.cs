using UnityEngine;
using Kart;

public class BaseKartComponent : MonoBehaviour
{
    protected KartEvents kartEvents;
    protected KartHub kartActions;
    protected void Awake()
    {
        kartEvents = GetComponentInParent<KartEvents>();
        kartActions = GetComponentInParent<KartHub>();
    }
}
