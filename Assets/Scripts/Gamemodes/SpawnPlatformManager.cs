using UnityEngine;
using Bolt;
using System.Collections;

namespace Gamemodes
{
    [DisallowMultipleComponent]
    public class SpawnPlatformManager : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private float _secondsActivated;

        [Header("Colliders")]
        [SerializeField] private Collider _collider;

        // BOLT


    }
}
