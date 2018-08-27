using UnityEngine;
using Capacities;
using Items;
using HUD;

namespace Kart
{
    /*
     * Main class to map the inputs to the different kart actions
     * The states should handled and modified within this class
     *
     */
    public class KartHub : BaseKartComponent
    {
        [HideInInspector] public KartEngine kartEngine;
        [HideInInspector] public KartDriftSystem kartDriftSystem;
        [HideInInspector] public KartInventory kartInventory;
        [HideInInspector] public KartHealthSystem kartHealthSystem;
        [HideInInspector] public Capacity kartCapacity;
        [HideInInspector] public CinemachineDynamicScript cinemachineDynamicScript;

        private int score = 0;

        private new void Awake()
        {
            base.Awake();

            kartEngine = GetComponentInChildren<KartEngine>();
            kartDriftSystem = GetComponentInChildren<KartDriftSystem>();
            kartInventory = GetComponentInChildren<KartInventory>();
            kartHealthSystem = GetComponentInChildren<KartHealthSystem>();
            kartCapacity = GetComponentInChildren<Capacity>();
            cinemachineDynamicScript = GetComponentInChildren<CinemachineDynamicScript>();

            KartEvents.Instance.HitSomeoneElse += IncreaseScore;
        }

        private void FixedUpdate()
        {
            if (kartStates.DriftTurnState == TurnState.NotTurning)
            {
                kartEngine.CompensateSlip();
            }
        }

        public void UseItem(float verticalValue)
        {
            if (verticalValue > 0.3f)
            {
                UseItemForward();
            }
            else if (verticalValue < -0.3f)
            {
                UseItemBackward();
            }
            else
            {
                kartInventory.ItemAction(Direction.Default);
            }
        }

        public void UseItemForward()
        {
            kartInventory.ItemAction(Direction.Forward);
        }

        public void UseItemBackward()
        {
            kartInventory.ItemAction(Direction.Backward);
        }

        public void UseCapacity(float xAxis, float yAxis)
        {
            kartCapacity.Use(xAxis, yAxis);
        }

        public void InitializeDrift(float angle)
        {
            kartDriftSystem.InitializeDrift(angle);
        }

        public void StopDrift()
        {
            kartDriftSystem.StopDrift();
        }

        public void DriftTurns(float turnValue)
        {
            if (!kartStates.IsGrounded()) return;

            if (kartStates.DriftTurnState != TurnState.NotTurning && kartDriftSystem.CheckRequiredSpeed())
            {
                kartEngine.DriftTurn(turnValue);
                kartDriftSystem.DriftForces();
                kartDriftSystem.CheckNewTurnDirection();
            }
            else if (kartStates.DriftTurnState == TurnState.NotTurning)
            {
                if (kartStates.TurningState == TurnState.Left)
                    kartStates.DriftTurnState = TurnState.Left;

                if (kartStates.TurningState == TurnState.Right)
                    kartStates.DriftTurnState = TurnState.Right;

                InitializeDrift(turnValue);
            }
        }

        public void Accelerate(float value)
        {
            if (kartStates.AirState != AirState.Air)
            {
                kartEngine.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirState.Air)
            {
                kartEngine.Decelerate(value);
            }
        }

        public void Turn(float turnValue)
        {
            float newTurnSensitivity = turnValue;
            kartEvents.OnTurn(turnValue);
            if (Mathf.Abs(turnValue) <= 0.9f)
            {
                newTurnSensitivity = turnValue / kartEngine.LowerTurnSensitivity;
            }

            SetTurnState(turnValue);

            if (kartStates.DriftTurnState == TurnState.NotTurning)
            {
                if (kartStates.AccelerationState == AccelerationState.Forward)
                {
                    kartEngine.TurnUsingTorque(Vector3.up * newTurnSensitivity, turnValue);
                }
                else if (kartStates.AccelerationState == AccelerationState.Back)
                {
                    kartEngine.TurnUsingTorque(Vector3.down * newTurnSensitivity, turnValue);
                }
            }
        }

        private void SetTurnState(float turnValue)
        {
            if (turnValue > 0)
            {
                kartStates.TurningState = TurnState.Right;
            }
            else if (turnValue < 0)
            {
                kartStates.TurningState = TurnState.Left;
            }
            else
            {
                kartStates.TurningState = TurnState.NotTurning;
            }
        }

        public void IncreaseScore()
        {
            score++;
            PhotonNetwork.player.SetScore(score);
            PhotonView view = GetComponent<PhotonView>();
            // TODO: Use RaiseEvent instead?
            view.RPC("UpdateScore", PhotonTargets.AllBuffered);
        }

        [PunRPC]
        void UpdateScore()
        {
            KartEvents.Instance.OnScoreChange();
        }
    }
}
