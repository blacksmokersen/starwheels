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

        public void InitializeDrift(float angle)
        {
            Jump(0.5f);
            kartDriftSystem.InitializeDrift(angle);
        }

        public void StopDrift()
        {
            kartDriftSystem.StopDrift();
        }

        public void DriftTurns(float angle)
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartPhysics.DriftUsingForce(Vector3.right, Vector3.back);
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartPhysics.DriftUsingForce(Vector3.right,Vector3.forward);
                }
                kartOrientation.DriftTurn(angle);
                kartDriftSystem.CheckNewTurnDirection();
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartPhysics.DriftUsingForce(Vector3.left, Vector3.forward);
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartPhysics.DriftUsingForce(Vector3.left, Vector3.back);
                }
                kartOrientation.DriftTurn(angle);
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
                //kartOrientation.Turn(value);
                kartPhysics.TurnUsingTorque(Vector3.up * value);
            }
        }
    }
}