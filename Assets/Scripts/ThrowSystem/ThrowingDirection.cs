using UnityEngine;

namespace ThrowingSystem
{
    public class ThrowingDirection : MonoBehaviour
    {
        public Direction LastDirectionUp
        {
            get
            {
                if (Input.GetButtonUp(Constants.Input.UseItemForward))
                {
                    return Direction.Forward;
                }
                else if (Input.GetButtonUp(Constants.Input.UseItemBackward))
                {
                    return Direction.Backward;
                }
                else if (Input.GetAxis(Constants.Input.UpAndDownAxis) > 0.3f)
                {
                    return Direction.Forward;
                }
                else if (Input.GetAxis(Constants.Input.UpAndDownAxis) < -0.3f)
                {
                    return Direction.Backward;
                }
                else
                {
                    return Direction.Default;
                }
            }
        }
        public Direction CurrentDirection
        {
            get
            {
                if (Input.GetButton(Constants.Input.UseItemForward))
                {
                    return Direction.Forward;
                }
                else if (Input.GetButton(Constants.Input.UseItemBackward))
                {
                    return Direction.Backward;
                }
                else if (Input.GetAxis(Constants.Input.UpAndDownAxis) > 0.3f)
                {
                    return Direction.Forward;
                }
                else if (Input.GetAxis(Constants.Input.UpAndDownAxis) < -0.3f)
                {
                    return Direction.Backward;
                }
                else
                {
                    return Direction.Default;
                }
            }
        }
    }
}
