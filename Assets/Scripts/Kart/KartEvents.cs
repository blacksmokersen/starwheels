using UnityEngine;
using System;
using Items;
using Photon.Pun;
using Photon.Realtime;

namespace Kart
{
    [RequireComponent(typeof(PhotonView))]
    public class KartEvents : MonoBehaviourPun
    {
        // Movements
        public Action<Vector3> OnVelocityChange;
        public Action<float> OnTurn;

        // Game
        public Action OnHit;
        public Action OnHitRecover;
        public Action<Player> OnHitSomeoneElse;
        public Action<int> OnHealthLoss;
        public Action OnKartDestroyed;

        // Collisions
        public Action OnCollisionEnterGround;

        // ItemBox
        public Action<ItemData> OnItemGet;
        public Action<int> OnItemCountChange;
        public Action<ItemData> OnItemUse;
        public Action OnItemBoxGet;
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
        public Action OnDriftBoostEnd;

        //Camera
        public Action<bool> OnBackCameraStart;
        public Action<bool> OnBackCameraEnd;
        public Action<float> OnCameraTurnStart;
        public Action OnCameraTurnReset;

        // RPCs
        [PunRPC] public void RPCOnHit() { OnHit(); }
        [PunRPC] public void RPCOnJump() { OnJump(); }
        [PunRPC] public void RPCOnDoubleJump(Direction direction) { OnDoubleJump(direction); }

        [PunRPC] public void RPCOnDriftStart() { OnDriftStart(); }
        [PunRPC] public void RPCOnDriftWhite() { OnDriftWhite(); }
        [PunRPC] public void RPCOnDriftOrange() { OnDriftOrange(); }
        [PunRPC] public void RPCOnDriftRed() { OnDriftRed(); }
        [PunRPC] public void RPCOnDriftEnd() { OnDriftEnd(); }
        [PunRPC] public void RPCOnDriftReset() { OnDriftReset(); }
        [PunRPC] public void RPCOnDriftBoostEnd() { OnDriftBoostEnd(); }
        [PunRPC] public void RPCOnDriftBoostStart() { OnDriftBoostStart(); }

        // CORE

        // PUBLIC
        public void CallRPC(string onAction, params object[] parameters)
        {
            photonView.RPC("RPC" + onAction, RpcTarget.All, parameters);
        }

        // PRIVATE
    }
}
