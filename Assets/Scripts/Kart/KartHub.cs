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
    public class KartHub : MonoBehaviour
    {
        [HideInInspector] public KartStates kartStates;
        [HideInInspector] public KartEngine kartEngine;
        [HideInInspector] public KartDriftSystem kartDriftSystem;
        [HideInInspector] public KartInventory kartInventory;
        [HideInInspector] public KartHealthSystem kartHealthSystem;
        [HideInInspector] public Capacity kartCapacity;
        [HideInInspector] public CinemachineDynamicScript cinemachineDynamicScript;

        private KartEvents kartEvents;
        private float driftMinSpeedActivation = 10f;
        private int score = 0;

        void Awake()
        {
            kartStates = GetComponent<KartStates>();
            kartEvents = GetComponent<KartEvents>();

            kartEngine = GetComponentInChildren<KartEngine>();
            kartDriftSystem = GetComponentInChildren<KartDriftSystem>();
            kartInventory = GetComponentInChildren<KartInventory>();
            kartHealthSystem = GetComponentInChildren<KartHealthSystem>();
            kartCapacity = GetComponentInChildren<Capacity>();
            cinemachineDynamicScript = GetComponentInChildren<CinemachineDynamicScript>();
            KartEvents.Instance.HitSomeoneElse += SetScore;
        }

        private void FixedUpdate()
        {
            if (kartStates.DriftTurnState == TurningStates.NotTurning)
            {
                kartEngine.CompensateSlip();
            }
        }

        public void UseItem(float verticalValue)
        {
            if (verticalValue > 0.3f)
                UseItemForward();
            else if (verticalValue < -0.3f)
                UseItemBackward();
            else
                kartInventory.ItemAction(Directions.Default);
        }

        public void UseItemForward()
        {
            kartInventory.ItemAction(Directions.Forward);
        }

        public void UseItemBackward()
        {
            kartInventory.ItemAction(Directions.Backward);
        }

        public void UseCapacity(float xAxis, float yAxis)
        {
            kartCapacity.Use(xAxis, yAxis);
        }

        public void InitializeDrift(float angle)
        {
            if (kartStates.IsGrounded() && kartEngine.PlayerVelocity >= driftMinSpeedActivation && angle != 0)
            {
                kartDriftSystem.InitializeDrift(angle);
            }
        }

        public void StopDrift()
        {
            kartDriftSystem.StopDrift();
        }

        public void DriftTurns(float turnValue)
        {
            if (!kartStates.IsGrounded()) return;

            if (kartStates.DriftTurnState != TurningStates.NotTurning && kartEngine.PlayerVelocity >= driftMinSpeedActivation)
            {
                kartEngine.DriftTurn(turnValue);
                kartDriftSystem.DriftForces();
                kartDriftSystem.CheckNewTurnDirection();
            }
            else if (kartStates.DriftTurnState == TurningStates.NotTurning)
            {
                if (kartStates.TurningState == TurningStates.Left)
                    kartStates.DriftTurnState = TurningStates.Left;

                if (kartStates.TurningState == TurningStates.Right)
                    kartStates.DriftTurnState = TurningStates.Right;

                InitializeDrift(turnValue);
            }
        }

        public void Accelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir)
            {
                kartEngine.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir)
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

            if (kartStates.DriftTurnState == TurningStates.NotTurning)
            {
                if (kartStates.AccelerationState == AccelerationStates.Forward)
                {
                    kartEngine.TurnUsingTorque(Vector3.up * newTurnSensitivity, turnValue);
                }
                else if (kartStates.AccelerationState == AccelerationStates.Back)
                {
                    kartEngine.TurnUsingTorque(Vector3.down * newTurnSensitivity, turnValue);
                }
            }
        }

        private void SetTurnState(float turnValue)
        {
            if (turnValue > 0)
            {
                kartStates.TurningState = TurningStates.Right;
            }
            else if (turnValue < 0)
            {
                kartStates.TurningState = TurningStates.Left;
            }
            else
            {
                kartStates.TurningState = TurningStates.NotTurning;
            }
        }

        public void SetScore()
        {
            score++;
            PhotonNetwork.player.SetScore(score);
            PhotonView view = GetComponent<PhotonView>();
            view.RPC("UpdateScore", PhotonTargets.AllBuffered);
        }

        [PunRPC]
        void UpdateScore()
        {
            GameObject.Find("HUD").GetComponent<HUDUpdater>().UpdatePlayerList();
        }
    }
}
