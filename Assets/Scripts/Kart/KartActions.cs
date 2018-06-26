using UnityEngine;

namespace Kart
{
    public class KartActions : MonoBehaviour
    {
        private KartPhysics kartPhysics;
        private KartOrientation kartOrientation;
        private KartStates kartStates;
        private KartDriftSystem kartDriftSystem;

        void Awake()
        {
            kartPhysics = GetComponentInParent<KartPhysics>();
            kartOrientation = GetComponentInParent<KartOrientation>();
            kartStates = GetComponentInParent<KartStates>();
            kartDriftSystem = GetComponentInParent<KartDriftSystem>();
        }

        public void Jump(float value = 1f)
        {
            if (kartStates.AirState == AirStates.Grounded)
            {
                kartPhysics.Jump(value);
                kartStates.AirState = AirStates.InAir;
            }
        }

        public void Fire()
        {
            Debug.Log("Firing");
        }

        public void InitializeDrift(float horizontal)
        {
            Jump(0.5f);
            if(horizontal < 0)
            {
                kartStates.DriftTurnState = DriftTurnStates.DriftingLeft;
            }
            if (horizontal > 0)
            {
                kartStates.DriftTurnState = DriftTurnStates.DriftingRight;
            }
            kartStates.DriftBoostState = DriftBoostStates.SimpleDrift;
        }
        
        public void DriftTurns()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartPhysics.Drift(Vector3.right, Vector3.back);
                    kartOrientation.QuickTurn();
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartPhysics.Drift(Vector3.right,Vector3.forward);
                    kartOrientation.SlowTurn();
                }
                kartDriftSystem.CheckNewTurnDirection();
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartPhysics.Drift(Vector3.left, Vector3.forward);
                    kartOrientation.SlowTurn();
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartPhysics.Drift(Vector3.left, Vector3.back);
                    kartOrientation.QuickTurn();
                }
                kartDriftSystem.CheckNewTurnDirection();
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
            kartStates.DriftBoostState = DriftBoostStates.NotDrifting;
        }

        public void Accelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir)
            {
                kartPhysics.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir)
            {
                kartPhysics.Decelerate(value);
            }
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