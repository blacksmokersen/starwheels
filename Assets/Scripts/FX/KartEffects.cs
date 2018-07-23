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
        public ParticleSystem Life1;
        public ParticleSystem LifeBurst1;
        public ParticleSystem Life2;
        public ParticleSystem LifeBurst2;
        public ParticleSystem Life3;
        public ParticleSystem LifeBurst3;
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
            if (health == 2)
            {
                Life1.Stop(true);
               // LifeBurst1.Emit(200);
                LifeBurst1.Play();
            }
            if (health == 1)
            {
                Life2.Stop(true);
                //  LifeBurst2.Emit(200);
                LifeBurst2.Play();
            }
            if (health == 0)
            {
                Life3.Stop(true);
                //  LifeBurst3.Emit(200);
                LifeBurst3.Play();
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

