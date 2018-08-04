using Photon;
using Kart;

public class PunBaseKartComponent : PunBehaviour
{
    protected KartEvents kartEvents;
    protected KartHub kartActions;
    protected void Awake()
    {
        kartEvents = GetComponentInParent<KartEvents>();
        kartActions = GetComponentInParent<KartHub>();
    }
}
