using UnityEngine;

namespace Kart
{
    public class KartActions : MonoBehaviour
    {
        private KartPhysics kartPhysics;
        private KartOrientation kartOrientation;
        private KartStates kartStates;

        void Awake()
        {
            kartPhysics = GetComponentInChildren<KartPhysics>();
            kartOrientation = GetComponentInChildren<KartOrientation>();
            kartStates = GetComponentInChildren<KartStates>();
        }

        public void Jump()
        {
            if (kartStates.AirState == KartStates.AirStates.Grounded)
            {
                kartPhysics.Jump();
                kartStates.AirState = KartStates.AirStates.InAir;
            }
        }

        public void Fire()
        {
            Debug.Log("Firing");
        }

        public void InitializeDrift(float horizontal)
        {
            if(horizontal < 0)
            {
                kartStates.DrifState = KartStates.DriftStates.DriftingLeft;
            }
            if (horizontal > 0)
            {
                kartStates.DrifState = KartStates.DriftStates.DriftingRight;
            }
        }
        
        public void Drift()
        {
            if (kartStates.DrifState == KartStates.DriftStates.DriftingLeft)
            {
                if (kartStates.TurningState == KartStates.TurningStates.Left)
                {
                    kartPhysics.Drift(Vector3.left, Vector3.down);
                    //kartOrientation.Turn(-0.8f);
                    kartOrientation.QuickTurn();
                }
                if (kartStates.TurningState == KartStates.TurningStates.Right)
                {
                    kartPhysics.Drift(Vector3.left,Vector3.up);
                    //kartOrientation.Turn(-0.45f);
                    kartOrientation.SlowTurn();
                }
            }
            else if (kartStates.DrifState == KartStates.DriftStates.DriftingRight)
            {
                if (kartStates.TurningState == KartStates.TurningStates.Left)
                {
                    kartPhysics.Drift(Vector3.right, Vector3.up);
                    //kartOrientation.Turn(0.45f);
                    kartOrientation.SlowTurn();
                }
                if (kartStates.TurningState == KartStates.TurningStates.Right)
                {
                    kartPhysics.Drift(Vector3.right, Vector3.down);
                    //kartOrientation.Turn(0.8f);
                    kartOrientation.QuickTurn();
                }
            }
        }

        public void StopDrift()
        {
            kartStates.DrifState = KartStates.DriftStates.NotDrifting;
        }

        public void Accelerate(float value)
        {
            kartPhysics.Accelerate(value);
        }

        public void Decelerate(float value)
        {
            kartPhysics.Decelerate(value);
        }

        public void Turn(float value)
        {
            if (value > 0)
            {
                kartStates.TurningState = KartStates.TurningStates.Right;
            }
            else if (value < 0)
            {
                kartStates.TurningState = KartStates.TurningStates.Left;
            }
            else
            {
                kartStates.TurningState = KartStates.TurningStates.NotTurning;
            }
            if (kartStates.DrifState == KartStates.DriftStates.NotDrifting)
            {
                kartOrientation.Turn(value);
            }
        }
    }
}