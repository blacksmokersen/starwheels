using UnityEngine;
using Bolt;
using Multiplayer;

public class KartDestroyer : GlobalEventListener
{
    [Header("Kart Root")]
    [SerializeField] private BoltEntity _rootEntity;

    [Header("Player Settings")]
    [SerializeField] private PlayerSettings _settings;

    public void DestroyKart()
    {
        BoltNetwork.Destroy(_rootEntity);
    }
}
