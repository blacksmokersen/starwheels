using UnityEngine;

namespace Kart { 
    public class KartStates : MonoBehaviour {

        public enum TurningStates { NotTurning, Left, Right }
        public enum DriftStates { NotDrifting, DriftingLeft, DriftingRight, Turbo }
        public enum AirStates { Grounded, Jumping, InAir }

        public TurningStates TurningState = TurningStates.NotTurning;
        public DriftStates DrifState = DriftStates.NotDrifting;
        public AirStates AirState = AirStates.Grounded;
    }
}
