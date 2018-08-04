using UnityEngine;
using Kart;

public class BaseKartComponent : MonoBehaviour
{
    protected KartEvents kartEvents;
    protected KartHub kartHub;
    protected void Awake()
    {
        kartEvents = GetComponentInParent<KartEvents>();
        kartHub = GetComponentInParent<KartHub>();
    }
}
