using UnityEngine;
using Bolt;
using Multiplayer;

public class KartDestroyer : GlobalEventListener
{
    [Header("Kart Root")]
    [SerializeField] private BoltEntity _rootEntity;

    [Header("Player Settings")]
    [SerializeField] private PlayerSettings _settings;

    public override void OnEvent(KartDestroyed evnt)
    {
        if (evnt.ConnectionID == _settings.ConnectionID)
        {
            BoltNetwork.Destroy(_rootEntity);
        }
    }
}
