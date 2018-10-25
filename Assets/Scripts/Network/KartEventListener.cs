using UnityEngine;
using Bolt;

public class KartEventListener : GlobalEventListener
{
    public override void OnEvent(PlayerHit evnt)
    {
        var kartEntity = GetComponent<BoltEntity>();
        if (kartEntity == evnt.PlayerEntity)
        {
            Debug.LogError("It's me !");
            kartEntity.GetComponentInChildren<Health.Health>().LoseHealth();
        }
    }
}
