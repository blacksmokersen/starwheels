using UnityEngine;
using Kart;

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
            ButtonsDown();
            ButtonsUp();
            ButtonsPressed();
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
            if (Input.GetButtonDown(Constants.FireButton))
            {
                kart.Fire();
            }
            if (Input.GetButtonDown(Constants.DriftButton))
            {
                kart.InitializeDrift(Input.GetAxis(Constants.TurnAxis));
            }
        }

        void ButtonsPressed()
        {
            if (Input.GetButton(Constants.DriftButton))
            {
                kart.DriftTurns();
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