﻿using Bolt;

public class HitEventListener : GlobalEventListener
{
    public override void OnEvent(PlayerHit evnt)
    {
        var kartEntity = GetComponent<BoltEntity>();
        if (kartEntity == evnt.PlayerEntity)
        {
            kartEntity.GetComponentInChildren<Health.Health>().LoseHealth();
        }
    }
}