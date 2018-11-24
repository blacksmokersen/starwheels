using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class ExplosionOnPlayerHit : GlobalEventListener
{
    public override void OnEvent(PlayerHit playerLaunchItem)
    {
        Debug.Log("TETETETETET");
        gameObject.SetActive(true);
    }
}
