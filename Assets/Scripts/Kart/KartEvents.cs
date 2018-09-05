using UnityEngine;
using System;
using Items;
using Photon.Pun;

namespace Kart
{
    public class KartEvents : MonoBehaviour
    {
        public static KartEvents Instance;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected || GetComponent<PhotonView>().IsMine)
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
        public Action OnKartDestroyed;
        public Action OnScoreChange;

        // Collisions
        public Action OnCollisionEnterGround;

        // ItemBox
        public Action OnGetItemBox;
        public Action OnLotteryStop;

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
        public Action OnDriftEnd;
        public Action OnDriftReset;
        public Action OnDriftBoostStart;
        public Action OnDrfitBoostEnd;
        public Action OnDriftNextState;

        //Camera
        public Action<bool> OnBackCameraStart;
        public Action<bool> OnBackCameraEnd;
        public Action<float> OnCameraTurnStart;
        public Action OnCameraTurnReset;

        public void CallOnHitEvent()
        {
            GetComponent<PhotonView>().RPC("RPCCallOnHitEvent", RpcTarget.All);
        }

        [PunRPC]
        public void RPCCallOnHitEvent()
        {
            OnHit();
        }
    }
}
