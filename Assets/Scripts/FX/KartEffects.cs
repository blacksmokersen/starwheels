using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kart;


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
    public Animator animator;

    private KartStates kartStates;
    

    private void Awake()
    {
        kartStates = FindObjectOfType<KartStates>();
        //   smokeLeftWheel = GameObject.Find("smoke1").GetComponent<ParticleSystem>();
        //   smokeRightWheel = GameObject.Find("smoke2").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        animator = GameObject.Find("KartPieces").GetComponent<Animator>();

        DriftSmokeControl();
     //   ResetAnimation();
      //  animator.enabled = false;
    }

    void DriftSmokeControl()
    {
        if (kartStates.DriftBoostState == DriftBoostStates.NotDrifting)
        {
            smokeLeftWheel.Stop(true);
            smokeRightWheel.Stop(true);
        }
        else if (kartStates.AirState == AirStates.Grounded)
        {
            var main = smokeLeftWheel.main;
            var main2 = smokeRightWheel.main;

            if (!smokeLeftWheel.isPlaying)
                smokeLeftWheel.Play(true);
            if (!smokeRightWheel.isPlaying)
                smokeRightWheel.Play(true);

            if (kartStates.DriftBoostState == DriftBoostStates.OrangeDrift)
            {
                main.startColor = new Color(255, 255, 0, 255);
                main2.startColor = new Color(255, 255, 0, 255);
            }
            else if (kartStates.DriftBoostState == DriftBoostStates.RedDrift)
            {
                main.startColor = new Color(255, 0, 0, 255);
                main2.startColor = new Color(255, 0, 0, 255);
            }
            else
            {
                main.startColor = new Color(255, 255, 255, 255);
                main2.startColor = new Color(255, 255, 255, 255);
            }
        }
    }

    public void HealthParticlesManagement(int health)
    {
        if (health == 2)
        {
            Life1.Stop(true);
            LifeBurst1.Emit(200);
        }
        if (health == 1)
        {
            Life2.Stop(true);
            LifeBurst2.Emit(200);
        }
        if (health == 0)
        {
            Life3.Stop(true);
            LifeBurst3.Emit(200);
        }
    }

    public void LeftJumpAnimation()
    {
      //  animator.SetBool("LeftJump", true);
        animator.SetTrigger("LeftJump");
    }
    public void RightJumpAnimation()
    {
      //  animator.SetBool("RightJump", true);
        animator.SetTrigger("RightJump");
    }
    public void FrontJumpAnimation()
    {
      //  animator.SetBool("FrontJump", true);
        animator.SetTrigger("FrontJump");
    }
    public void BackJumpAnimation()
    {
      //  animator.SetBool("BackJump", true);
        animator.SetTrigger("BackJump");
    }

    public void ResetAnimation()
    {
        animator.SetBool("LeftJump", false);
        animator.SetBool("RightJump", false);
        animator.SetBool("FrontJump", false);
        animator.SetBool("BackJump", false);
    }
}

