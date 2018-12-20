using Bolt;

public class HitEventListener : GlobalEventListener
{
    public override void OnEvent(PlayerHit evnt)
    {
        var kartEntity = GetComponent<BoltEntity>();
        if (kartEntity == evnt.VictimEntity)
        {
            kartEntity.GetComponentInChildren<Health.Health>().LoseHealth();
        }
    }

    public override void OnEvent(DestroyEntity evnt)
    {
        if (evnt.Entity != null
            && evnt.Entity.isOwner)
        {
            BoltNetwork.Destroy(evnt.Entity);
        }
    }
}
