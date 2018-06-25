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
            if (kartStates.DrifState == KartStates.DriftStates.NotDrifting)
            {
                transform.Rotate(new Vector3(0, 0, value * TurningSpeed * Time.deltaTime));
            }
            if (kartStates.DrifState == KartStates.DriftStates.DriftingRight || kartStates.DrifState == KartStates.DriftStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, 0, value * DriftingTurningSpeed * Time.deltaTime));
            }
        }
        
        // Same behaviour as Turn() but with predefined value
        public void SlowTurn()
        {
            if (kartStates.DrifState == KartStates.DriftStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, 0, -SlowTurnValue * DriftingTurningSpeed * Time.deltaTime));
            }
            else if (kartStates.DrifState == KartStates.DriftStates.DriftingRight)
            {
                transform.Rotate(new Vector3(0, 0, SlowTurnValue * DriftingTurningSpeed * Time.deltaTime));
            }
        }

        // Same behaviour as Turn() but with predefined value
        public void QuickTurn()
        {
            if (kartStates.DrifState == KartStates.DriftStates.DriftingLeft)
            {
                transform.Rotate(new Vector3(0, 0, -QuickTurnValue * DriftingTurningSpeed * Time.deltaTime));
            }
            else if (kartStates.DrifState == KartStates.DriftStates.DriftingRight)
            {
                transform.Rotate(new Vector3(0, 0, QuickTurnValue * DriftingTurningSpeed * Time.deltaTime));
            }
        }        
    }
}