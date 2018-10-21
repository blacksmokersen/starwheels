using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform FrontPosition;
    public Transform BackPosition;

    [SerializeField] private ProjectileLauncherSettings settings;

    public Direction Direction
    {
        get
        {
            float directionAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);

            if (directionAxis > settings.ForwardTreshold)
                return Direction.Forward;
            else if (directionAxis < settings.BackwardTreshold)
                return Direction.Backward;
            else
                return Direction.Default;
        }
    }

    public float AimAxis
    {
        get
        {
            return Input.GetAxis(Constants.Input.ItemAimHorinzontal);
        }
    }

    // CORE

    // PUBLIC

    // PRIVATE
}
