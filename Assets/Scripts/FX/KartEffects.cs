using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kart
{
    public class KartEffects : MonoBehaviour
    {
        public ParticleSystem smokeLeftWheel;
        public ParticleSystem smokeRightWheel;
        [Space(10)]
        // TODO: 
        // public LifeCapsule[] capsules;
        // public void SetLife(int value) { value * capules; }
        public ParticleSystem[] Lifes;
        public ParticleSystem[] LifeBursts;
        [Space(10)]
        public ParticleSystem MainJump;
        public ParticleSystem JumpReload;
        [Space(10)]
        public Animator animator;

        public void StopSmoke()
        {
            smokeLeftWheel.Stop(true);
            smokeRightWheel.Stop(true);
        }
        public void StartSmoke()
        {
            if (!smokeLeftWheel.isPlaying)
                smokeLeftWheel.Play(true);
            if (!smokeRightWheel.isPlaying)
                smokeRightWheel.Play(true);
        }

        public void HealthParticlesManagement(int health)
        {
            Lifes[health].Stop(true);
            LifeBursts[health].Play();
        }

        public void ResetLife()
        {
            foreach (ParticleSystem ps in Lifes)
            {
                ps.Play();
            }
        }

        public void MainJumpParticles(int number)
        {
            MainJump.Emit(number);
        }

        public void ReloadJump()
        {
            JumpReload.Emit(300);
        }

        // TODO: Move to KartAnimations.cs
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
        public void BackJumpAnimation()
        {
            animator.SetTrigger("BackJump");
        }

        public void SetColor(Color color)
        {
            var main = smokeLeftWheel.main;
            var main2 = smokeRightWheel.main;

            main.startColor = color;
            main2.startColor = color;
        }
    }
}

