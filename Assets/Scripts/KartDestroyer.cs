using UnityEngine;
using Bolt;
using Multiplayer;
using System.Collections;

public class KartDestroyer : EntityBehaviour
{
    [Header("Kart Root")]
    [SerializeField] private BoltEntity _rootEntity;

    [Header("Player Settings")]
    [SerializeField] private PlayerSettings _settings;

    // PUBLIC

    public void DestroyKart()
    {
        if (entity.isOwner)
        {
            BoltNetwork.Destroy(_rootEntity);
        }
    }

    public void DestroyKartAfterXSeconds(float x)
    {
        StartCoroutine(DestroyKartAfterXSecondsRoutine(x));
    }

    // PRIVATE

    private IEnumerator DestroyKartAfterXSecondsRoutine(float x)
    {
        yield return new WaitForSeconds(x);
        DestroyKart();
    }
}
