using UnityEngine;
using Kart;

public class BaseKartComponent : MonoBehaviour
{
    protected KartEvents kartEvents;
    protected KartStates kartStates;
    protected KartHub kartHub;
    protected PhotonView photonView;

    protected void Awake()
    {
        kartEvents = GetComponentInParent<KartEvents>();
        kartStates = GetComponentInParent<KartStates>();
        kartHub = GetComponentInParent<KartHub>();
        photonView = GetComponentInParent<PhotonView>();
    }
}
