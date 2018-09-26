using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundCondition))]
public class SteeringWheel : MonoBehaviour
{
    public enum TurnState { NotTurning, Left, Right }

    [Header("Turn Torques")]
    public SteeringWheelSettings Settings;

    [Header("States")]
    public TurnState TurningState = TurnState.NotTurning;
    public bool InverseDirections = false;

    [Header("Events")]
    public UnityEvent<TurnState> OnTurn;

    private Rigidbody _rb;
    private GroundCondition _groundCondition;

    // CORE

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundCondition = GetComponent<GroundCondition>();
    }

    // PUBLIC

    public void TurnUsingTorque(float turnValue)
    {
        SetTurnState(turnValue);
        if (_groundCondition.Grounded)
        {
            _rb.AddRelativeTorque(Vector3.up  * turnValue * Settings.TurnTorque, ForceMode.Force);
            OnTurn.Invoke(TurningState);
        }
    }

    // PRIVATE

    public void SetTurnState(float turnValue)
    {
        if (turnValue > 0)
        {
            TurningState = TurnState.Right;
        }
        else if (turnValue < 0)
        {
            TurningState = TurnState.Left;
        }
        else
        {
            TurningState = TurnState.NotTurning;
        }
    }
}
