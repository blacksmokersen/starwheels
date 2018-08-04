using UnityEngine;
using Kart;

namespace Controls
{
    /* 
     * Class for handling player inputs
     */
    public class PlayerInputs : BaseKartComponent
    {
        KartHub kartAction;

        public void SetKart(KartHub value)
        {
            kartAction = value;
        }

        void FixedUpdate()
        {
            if (kartAction == null) return;

            Axis();
            ButtonsPressed();
        }

        private void Update()
        {
            if (kartAction == null) return;

            ButtonsDown();
            ButtonsUp();
        }

        void Axis()
        {
            kartAction.Accelerate(Input.GetAxis(Constants.AccelerateButton));
            kartAction.Decelerate(Input.GetAxis(Constants.DecelerateButton));
            kartAction.Turn(Input.GetAxis(Constants.TurnAxis));
        }

        void ButtonsDown()
        {
            if (Input.GetButtonDown(Constants.SpecialCapacity))
            {
                kartAction.UseCapacity(Input.GetAxis(Constants.TurnAxis), Input.GetAxis(Constants.UpAndDownAxis));
            }
            if (Input.GetButtonDown(Constants.DriftButton))
            {
                kartAction.InitializeDrift(Input.GetAxis(Constants.TurnAxis));
            }
            if (Input.GetButtonDown(Constants.UseItemButton))
            {
                kartAction.UseItem(Input.GetAxis(Constants.UpAndDownAxis));
            }
        }

        void ButtonsPressed()
        {
            if (Input.GetButton(Constants.DriftButton))
            {
                kartAction.DriftTurns(Input.GetAxis(Constants.TurnAxis));
            }
        }

        void ButtonsUp()
        {
            if (Input.GetButtonUp(Constants.DriftButton))
            {
                kartAction.StopDrift();
            }
        }
    }
}