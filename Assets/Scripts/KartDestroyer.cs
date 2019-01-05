using UnityEngine;
using Bolt;
using Multiplayer;

public class KartDestroyer : EntityBehaviour
{
    [Header("Kart Root")]
    [SerializeField] private BoltEntity _rootEntity;

    [Header("Player Settings")]
    [SerializeField] private PlayerSettings _settings;

    public void DestroyKart()
    {
        if (entity.isOwner)
        {
            BoltNetwork.Destroy(_rootEntity);
        }
    }
}
