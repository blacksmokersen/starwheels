using UnityEngine;
using Kart;

namespace Controls
{
    /* 
     * Class for handling player inputs
     */
    public class PlayerInputs : BaseKartComponent
    {
        public bool DisableUseItem;
        public bool DisableMovement;

        private CinemachineDynamicScript cinemachineDynamicScript;

        public void SetKart(KartHub value)
        {
            kartHub = value;
        }

        private new void Awake()
        {
            base.Awake();
            cinemachineDynamicScript = kartHub.cinemachineDynamicScript;
        }

        void FixedUpdate()
        {
            if (kartHub == null) return;

            Axis();
            ButtonsPressed();
        }

        private void Update()
        {
            if (kartHub == null) return;

            ButtonsDown();
            ButtonsUp();
        }

        void Axis()
        {
            if (!DisableMovement)
            {
                kartHub.Accelerate(Input.GetAxis(Constants.AccelerateButton));
                kartHub.Decelerate(Input.GetAxis(Constants.DecelerateButton));
                kartHub.Turn(Input.GetAxis(Constants.TurnAxis));
            }
        }

        void ButtonsDown()
        {
            if (Input.GetButtonDown(Constants.SpecialCapacity))
            {
                kartHub.UseCapacity(Input.GetAxis(Constants.TurnAxis), Input.GetAxis(Constants.UpAndDownAxis));
            }
            if (Input.GetButtonDown(Constants.DriftButton))
            {
                kartHub.InitializeDrift(Input.GetAxis(Constants.TurnAxis));
            }
            if (Input.GetButtonDown(Constants.UseItemButton) && !DisableUseItem)
            {
                kartHub.UseItem(Input.GetAxis(Constants.UpAndDownAxis));                
            }
        }

        void ButtonsPressed()
        {
            if (Input.GetButton(Constants.DriftButton))
            {
                kartHub.DriftTurns(Input.GetAxis(Constants.TurnAxis));
            }
            if (Input.GetButton(Constants.BackCamera))
            {
                cinemachineDynamicScript.BackCamera(true);
            }
            else
                cinemachineDynamicScript.BackCamera(false);
        }

        void ButtonsUp()
        {
            if (Input.GetButtonUp(Constants.DriftButton))
            {
                kartHub.StopDrift();
            }
        }
    }
}