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
            if (!PhotonNetwork.connected || GetComponent<PhotonView>().isMine)
            {
                Instance = this;
            }
        }

        // Movements
        public Action<Vector3> OnVelocityChange;
        public Action<float> OnTurn;

        // Game
        public Action<float> OnEnergyConsumption;
        public Action<ItemData, int> OnItemUsed;
        public Action OnHit;
        public Action OnHitRecover;
        public Action HitSomeoneElse;
        public Action<int> OnHealthLoss;
        public Action OnScoreChange;

        // Collisions
        public Action OnCollisionEnterGround;
        public Action OnCollisionEnterItemBox;

        // Jumping Capacity
        public Action OnJump;
        public Action<Direction> OnDoubleJump;
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
        public Action OnDriftNextState;

        public void CallOnHitEvent()
        {
            GetComponent<PhotonView>().RPC("RPCCallOnHitEvent", PhotonTargets.All);
        }

        [PunRPC]
        public void RPCCallOnHitEvent()
        {
            OnHit();
        }
    }
}
