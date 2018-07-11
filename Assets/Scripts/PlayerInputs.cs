using UnityEngine;
using Kart;
using Items;

namespace Controls
{
    /* 
     * Class for handling player inputs
     */
    public class PlayerInputs : MonoBehaviour
    {

        KartActions kart;

        void Awake()
        {
            kart = FindObjectOfType<KartActions>();
        }

        void FixedUpdate()
        {
            Axis();
            ButtonsPressed();
        }

        private void Update()
        {
            ButtonsDown();
            ButtonsUp();
        }

        void Axis()
        {
            kart.Accelerate(Input.GetAxis(Constants.AccelerateButton));
            kart.Decelerate(Input.GetAxis(Constants.DecelerateButton));
            kart.Turn(Input.GetAxis(Constants.TurnAxis));
        }

        void ButtonsDown()
        {
            if (Input.GetButtonDown(Constants.JumpButton))
            {
                kart.Jump();
            }
            if (Input.GetButtonDown(Constants.DriftButton))
            {
                kart.InitializeDrift(Input.GetAxis(Constants.TurnAxis));
            }
            if (Input.GetButtonDown(Constants.UseItemButton))
            {
                kart.UseItem(Input.GetAxis(Constants.VerticalAxis));
            }
        }

        void ButtonsPressed()
        {
            if (Input.GetButton(Constants.DriftButton))
            {
                kart.DriftTurns(Input.GetAxis(Constants.TurnAxis));
            }
        }

        void ButtonsUp()
        {
            if (Input.GetButtonUp(Constants.DriftButton))
            {
                kart.StopDrift();
            }
        }
    }
}