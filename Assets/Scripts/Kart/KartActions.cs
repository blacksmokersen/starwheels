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
            if (kartStates.AirState == AirStates.Grounded)
            {
                kartPhysics.Jump();
                kartStates.AirState = AirStates.InAir;
            }
        }

        public void Fire()
        {
            Debug.Log("Firing");
        }

        public void InitializeDrift(float horizontal)
        {
            kartPhysics.Jump(0.5f);
            if(horizontal < 0)
            {
                kartStates.DriftTurnState = DriftTurnStates.DriftingLeft;
            }
            if (horizontal > 0)
            {
                kartStates.DriftTurnState = DriftTurnStates.DriftingRight;
            }
        }
        
        public void Drift()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartPhysics.Drift(Vector3.left, Vector3.down);
                    kartOrientation.QuickTurn();
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartPhysics.Drift(Vector3.left,Vector3.up);
                    kartOrientation.SlowTurn();
                }
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartPhysics.Drift(Vector3.right, Vector3.up);
                    kartOrientation.SlowTurn();
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartPhysics.Drift(Vector3.right, Vector3.down);
                    kartOrientation.QuickTurn();
                }
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartStates.DriftTurnState = DriftTurnStates.DriftingLeft;
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartStates.DriftTurnState = DriftTurnStates.DriftingRight;
                }
            }
        }

        public void StopDrift()
        {
            kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
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
                kartStates.TurningState = TurningStates.Right;
            }
            else if (value < 0)
            {
                kartStates.TurningState = TurningStates.Left;
            }
            else
            {
                kartStates.TurningState = TurningStates.NotTurning;
            }
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                kartOrientation.Turn(value);
            }
        }
    }
}