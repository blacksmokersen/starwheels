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
        public GameObject EscapeMenu;

        // CORE

        private new void Awake()
        {
            base.Awake();

            kartEvents.OnHit += () => SetInputEnabled(false);
            kartEvents.OnHitRecover += () => SetInputEnabled(true);
        }

        void FixedUpdate()
        {
            if (!photonView.IsMine || !Enabled || kartHub == null) return;

            Axis();
            ButtonsPressed();
        }

        private void Update()
        {
            if (!photonView.IsMine || !Enabled || kartHub == null) return;

            ButtonsDown();
            ButtonsUp();
            AxisOnUse();
        }

        // PUBLIC

        // PRIVATE

        private void SetInputEnabled(bool b)
        {
            Enabled = b;
        }

        private void Axis()
        {
            kartHub.Accelerate(Input.GetAxis(Constants.Input.Accelerate));
            kartHub.Decelerate(Input.GetAxis(Constants.Input.Decelerate));
            kartHub.Turn(Input.GetAxis(Constants.Input.TurnAxis));
            kartHub.ItemAim(Input.GetAxis(Constants.Input.ItemAimHorinzontal));
        }

        private void ButtonsDown()
        {
            // Keyboard & GamePad
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                kartHub.UseAbility(Input.GetAxis(Constants.Input.TurnAxis), Input.GetAxis(Constants.Input.UpAndDownAxis));
            }
            if (Input.GetButtonDown(Constants.Input.Drift))
            {
                kartHub.InitializeDrift(Input.GetAxis(Constants.Input.TurnAxis));
            }
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {
                kartHub.UseItem(Input.GetAxis(Constants.Input.UpAndDownAxis));
            }
            if (Input.GetButtonDown(Constants.Input.BackCamera))
            {
                kartEvents.OnBackCameraStart(true);
            }
            if (Input.GetButtonDown(Constants.Input.ResetCamera))
            {
                kartEvents.OnCameraTurnReset();
            }

            // Mouse
            if (Input.GetButtonDown(Constants.Input.UseItemForward))
            {
                kartHub.UseItemForward();
            }
            if (Input.GetButtonDown(Constants.Input.UseItemBackward))
            {
                kartHub.UseItemBackward();
            }
        }

        private void ButtonsPressed()
        {
            if (Input.GetButton(Constants.Input.Drift))
            {
                kartHub.DriftTurns(Input.GetAxis(Constants.Input.TurnAxis));
            }
        }

        private void ButtonsUp()
        {
            if (Input.GetButtonUp(Constants.Input.Drift))
            {
                kartHub.StopDrift();
            }
            if (Input.GetButtonUp(Constants.Input.BackCamera))
            {
                kartEvents.OnBackCameraEnd(false);
            }
        }


        public void AxisOnUse()
        {
            if (kartEvents.OnCameraTurnStart != null)
            {
                kartEvents.OnCameraTurnStart(Input.GetAxis(Constants.Input.ItemAimHorinzontal), Input.GetAxis(Constants.Input.ItemAimVertical));
            }
        }

    }
}
