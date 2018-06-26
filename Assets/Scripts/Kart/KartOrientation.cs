using UnityEngine;

namespace Kart
{
    /*
     * Class for handling the Kart orientation (drift, turn etc.)
     */ 
    public class KartOrientation : MonoBehaviour
    {
        private KartStates kartStates;

        public float TurningSpeed;
        public float DriftingTurningSpeed;
        public float SlowTurnValue;
        public float QuickTurnValue;

        private void Awake()
        {
            kartStates = GetComponentInParent<KartStates>();
        }

        public void Turn(float value)
        {
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                transform.Rotate(new Vector3(0, value * TurningSpeed * Time.deltaTime, 0));
            }
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight || kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, value * DriftingTurningSpeed * Time.deltaTime, 0));
            }
        }
        
        // Same behaviour as Turn() but with predefined value
        public void SlowTurn()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, -SlowTurnValue * DriftingTurningSpeed * Time.deltaTime, 0));
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                transform.Rotate(new Vector3(0, SlowTurnValue * DriftingTurningSpeed * Time.deltaTime, 0));
            }
        }

        // Same behaviour as Turn() but with predefined value
        public void QuickTurn()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, -QuickTurnValue * DriftingTurningSpeed * Time.deltaTime, 0));
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                transform.Rotate(new Vector3(0, QuickTurnValue * DriftingTurningSpeed * Time.deltaTime, 0));
            }
        }        
    }
}