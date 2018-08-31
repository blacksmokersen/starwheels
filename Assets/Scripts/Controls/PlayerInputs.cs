using UnityEngine;
using Kart;

namespace Controls
{
    /*
     * Class for handling player inputs
     */
    public class PlayerInputs : BaseKartComponent
    {
        public bool Enabled = true;
        public bool DisableUseItem;
        public bool DisableMovement;
        public GameObject EscapeMEnu;

        private CinemachineDynamicScript cinemachineDynamicScript;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnHit += () => Enabled = false;
            kartEvents.OnHitRecover += () => Enabled = true;
        }

        private void Start()
        {
            cinemachineDynamicScript = kartHub.cinemachineDynamicScript;
        }

        void FixedUpdate()
        {
            if (Enabled && kartHub != null && photonView.isMine)
            {
                Axis();
                ButtonsPressed();
            }
        }

        private void Update()
        {
            if (Enabled && kartHub != null && photonView.isMine)
            {
                ButtonsDown();
                ButtonsUp();
                AxisOnUse();
            }
        }

        void Axis()
        {
            kartHub.Accelerate(Input.GetAxis(Constants.AccelerateButton));
            kartHub.Decelerate(Input.GetAxis(Constants.DecelerateButton));
            kartHub.Turn(Input.GetAxis(Constants.TurnAxis));
        }

        void ButtonsDown()
        {
            // Keyboard & GamePad
            if (Input.GetButtonDown(Constants.SpecialCapacity))
            {
                kartHub.UseCapacity(Input.GetAxis(Constants.TurnAxis), Input.GetAxis(Constants.UpAndDownAxis));
            }
            if (Input.GetButtonDown(Constants.DriftButton))
            {
                kartHub.InitializeDrift(Input.GetAxis(Constants.TurnAxis));
            }
            if (Input.GetButtonDown(Constants.UseItemButton))
            {
                kartHub.UseItem(Input.GetAxis(Constants.UpAndDownAxis));
            }
            if (Input.GetButtonDown(Constants.BackCamera))
            {
                cinemachineDynamicScript.BackCamera(true);
            }

            // Mouse
            if (Input.GetButtonDown(Constants.UseItemForwardButton))
            {
                kartHub.UseItemForward();
            }
            if (Input.GetButtonDown(Constants.UseItemBackwardButton))
            {
                kartHub.UseItemBackward();
            }
        }

        void ButtonsPressed()
        {
            if (Input.GetButton(Constants.DriftButton))
            {
                kartHub.DriftTurns(Input.GetAxis(Constants.TurnAxis));
            }
        }

        void ButtonsUp()
        {
            if (Input.GetButtonUp(Constants.DriftButton))
            {
                kartHub.StopDrift();
            }
            if (Input.GetButtonUp(Constants.BackCamera))
            {
                cinemachineDynamicScript.BackCamera(false);
            }
        }
        void AxisOnUse()
        {
          //  cinemachineDynamicScript.TurnCamera(Input.GetAxis(Constants.TurnCamera));
        }
    }
}
