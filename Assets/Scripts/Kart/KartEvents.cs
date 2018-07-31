using UnityEngine;
using System;
using Items;

namespace Kart
{
    public class KartEvents : MonoBehaviour
    {
        public Action<float> OnVelocityChange;
        public Action<ItemData, int> OnItemUsed;
        public Action OnHit;

        // Collisions
        public Action OnCollisionEnterGround;
        public Action OnCollisionEnterItemBox;
        public Action OnCollisionItemBox;

        // Jumping Capacity
        public Action OnJump;
        public Action<Directions> OnDoubleJump;
        public Action OnDoubleJumpReset;

        // Drifting
        public Action OnDriftStart;
        public Action OnDrifting;
        public Action OnDriftEnd;
        public Action OnDriftBoost;
        public Action OnDriftReset;
        public Action OnDriftNextState;
    }
}
