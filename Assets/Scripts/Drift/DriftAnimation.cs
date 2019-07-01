using UnityEngine;
using SW.Customization;

namespace Drift
{
    public class DriftAnimation : Bolt.EntityBehaviour<IKartState>
    {
        [SerializeField] private Animator _animator;

        [Header("CharacterSetter")]
        [SerializeField] private CharacterSetter _characterSetter;

        public void LeftDriftAnimation()
        {
            if (entity.isAttached)
            {
              //  state.DriftLeft = true;
                _animator.SetBool("DriftLeft", true);
                _characterSetter.CurrentCharacterAnimator.SetBool("DriftLeft", true);
            }
        }

        public void RightDriftAnimation()
        {
            if (entity.isAttached)
            {
              //  state.DriftRight = true;
                _animator.SetBool("DriftRight", true);
                _characterSetter.CurrentCharacterAnimator.SetBool("DriftRight", true);
            }
        }

        public void NoDriftAnimation()
        {
            if (entity.isAttached)
            {
                //state.DriftLeft = false;
                _animator.SetBool("DriftLeft", false);
                _characterSetter.CurrentCharacterAnimator.SetBool("DriftLeft", false);
              //  state.DriftRight = false;
                _animator.SetBool("DriftRight", false);
                _characterSetter.CurrentCharacterAnimator.SetBool("DriftRight", false);
            }
        }

        public void BoostDriftAnimation()
        {
            if (entity.isAttached)
            {
                _characterSetter.CurrentCharacterAnimator.SetTrigger("HelloAnimation");
            }
        }
    }
}
