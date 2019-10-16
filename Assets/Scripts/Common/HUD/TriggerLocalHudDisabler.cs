using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Multiplayer;
using Bolt;

public class TriggerLocalHudDisabler : MonoBehaviour
{
    public UnityEvent PermanentDeathLocalTriggerEvent;

    //CORE

    private void OnTriggerEnter(Collider other)
    {
        /*
        Debug.LogError("1");
        if (other.CompareTag(Constants.Tag.KartCollider))
        {
        Debug.LogError("2");
            PermanentDeathLocalTriggerEvent.Invoke();
        }
        */
    }
}
