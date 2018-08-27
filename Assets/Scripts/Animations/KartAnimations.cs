using UnityEngine;
using Kart;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class KartAnimations : BaseKartComponent
    {
        private Animator animator;

        private new void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();

            if (photonView.isMine)
            {
                KartEvents.Instance.OnDoubleJump += DoubleJumpAnimation;
                KartEvents.Instance.OnHealthLoss += (a) => PlayerHitAnimation();
                KartEvents.Instance.OnDriftLeft += LeftDriftAnimation;
                KartEvents.Instance.OnDriftRight += RightDriftAnimation;
                KartEvents.Instance.OnDriftEnd += NoDriftAnimation;
                KartEvents.Instance.OnDriftBoost += NoDriftAnimation;
            }
        }

        public void DoubleJumpAnimation(Directions direction)
        {
            photonView.RPC("RPCDoubleJumpAnimation", PhotonTargets.All,direction);
        }


        [PunRPC]
        public void RPCDoubleJumpAnimation(Directions direction)
        {
            switch (direction)
            {
                case Directions.Forward:
                    FrontJumpAnimation();
                    break;
                case Directions.Backward:
                    BackJumpAnimation();
                    break;
                case Directions.Left:
                    LeftJumpAnimation();
                    break;
                case Directions.Right:
                    RightJumpAnimation();
                    break;
            }
        }

        public void LeftJumpAnimation()
        {
            animator.SetTrigger("LeftJump");
        }
        public void RightJumpAnimation()
        {
            animator.SetTrigger("RightJump");
        }
        public void FrontJumpAnimation()
        {
            animator.SetTrigger("FrontJump");
        }
        public void LeftDriftAnimation()
        {
            animator.SetBool("DriftLeft", true);
        }
        public void RightDriftAnimation()
        {
            animator.SetBool("DriftRight", true);
        }
        public void NoDriftAnimation()
        {
            animator.SetBool("DriftLeft", false);
            animator.SetBool("DriftRight", false);
        }
        public void BackJumpAnimation()
        {
            animator.SetTrigger("BackJump");
        }
        public void PlayerHitAnimation()
        {
            if (kartHub.kartEngine.PlayerVelocity >= 10)
                animator.SetTrigger("HitHighSpeed");
            else
                animator.SetTrigger("HitLowSpeed");
        }
    }
}
