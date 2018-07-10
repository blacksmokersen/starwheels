using UnityEngine;
using Kart;

namespace Controls
{
    /* 
     * Class for handling player inputs
     */
    public class PlayerInputs : MonoBehaviour
    {
        KartActions kartAction;

        public void SetKart(KartActions value)
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
            if (Input.GetButtonDown(Constants.JumpButton))
            {
                kartAction.Jump(3 , Input.GetAxis(Constants.TurnAxis), Input.GetAxis(Constants.AccelerateButton));
            }
            if (Input.GetButtonDown(Constants.FireButton))
            {
                kartAction.Fire();
            }
            if (Input.GetButtonDown(Constants.DriftButton))
            {
                kartAction.InitializeDrift(Input.GetAxis(Constants.TurnAxis));
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