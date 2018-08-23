using UnityEngine;
using System;
using Items;

namespace Kart
{
    public class KartEvents : MonoBehaviour
    {
        public static KartEvents Instance;
        private void Awake()
        {
            PhotonView view = GetComponent<PhotonView>();
            if (view.isMine || !PhotonNetwork.connected)
            {
                Instance = this;
            }
        }

        public Action<Vector3> OnVelocityChange;
        public Action<float> OnEnergyConsumption;
        public Action<float> OnTurn;
        public Action<ItemData, int> OnItemUsed;
        public Action OnHit;
        public Action OnHitRecover;
        public Action HitSomeoneElse;
        public Action<int> OnHealthLoss;

        // Collisions
        public Action OnCollisionEnterGround;
        public Action OnCollisionEnterItemBox;

        // Jumping Capacity
        public Action OnJump;
        public Action<Directions> OnDoubleJump;
        public Action OnDoubleJumpReset;

        // Drifting
        public Action OnDriftStart;
        public Action OnDriftLeft;
        public Action OnDriftRight;
        public Action OnDriftWhite;
        public Action OnDriftOrange;
        public Action OnDriftRed;
        public Action OnDrifting;
        public Action OnDriftEnd;
        public Action OnDriftBoost;
        public Action OnDriftReset;
        public Action OnDriftNextState;
    }
}
