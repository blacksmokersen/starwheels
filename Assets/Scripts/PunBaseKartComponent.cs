using Photon;
using Kart;

public class PunBaseKartComponent : PunBehaviour {

    protected KartEvents kartEvents;
    protected KartActions kartActions;
    protected void Awake()
    {
        kartEvents = GetComponentInParent<KartEvents>();
        kartActions = GetComponentInParent<KartActions>();
    }
}
