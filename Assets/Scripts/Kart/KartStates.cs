using UnityEngine;

namespace Kart {
    /*
     * States : 
     * - Turning 
     * - Drift 
     * - Air
     * - Acceleration ?
     */
    public enum TurningStates { NotTurning, Left, Right }
    public enum DriftTurnStates { NotDrifting, DriftingLeft, DriftingRight }
    public enum DriftBoostStates { NotDrifting, SimpleDrift, OrangeDrift, RedDrift, Turbo }
    public enum AirStates { Grounded, Jumping, InAir }

    public class KartStates : MonoBehaviour{

        public TurningStates TurningState = TurningStates.NotTurning;
        public DriftTurnStates DriftTurnState = DriftTurnStates.NotDrifting;
        public DriftBoostStates DriftBoostState = DriftBoostStates.NotDrifting;
        public AirStates AirState = AirStates.Grounded;
    }
}