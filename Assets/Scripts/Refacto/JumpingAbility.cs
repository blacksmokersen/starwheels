using System.Collections;
using UnityEngine;

public class JumpingAbility : MonoBehaviour, IControllable
{
    [Header("Forces")]
    public JumpingAbilitySettings Settings;

    private Rigidbody _rb;
    private bool _canUseAbility;
    private bool _hasDoneFirstJump;

    // CORE

    private void Awake ()
    {
        _rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
    {
        MapInputs();
	}

    // PUBLIC

    public void FirstJump()
    {
        _rb.AddRelativeForce(Vector3.up * Settings.FirstJumpForce, ForceMode.Impulse);
        _hasDoneFirstJump = true;
    }

    public void SecondJump(JoystickValues joystickValues)
    {
        Vector3 direction;
        if (joystickValues.X < -0.5f)
            direction = Vector3.left;
        else if (joystickValues.X > 0.5f)
            direction = Vector3.right;
        else if (joystickValues.Y > 0.5f)
            direction = Vector3.forward;
        else if (joystickValues.Y < -0.5f)
            direction = Vector3.back;
        else
            direction = Vector3.up;

        var forceUp = Vector3.up * Settings.SecondJumpUpForce;
        var forceDirectional = direction * Settings.SecondJumpLateralForces;
        _rb.AddRelativeForce(forceUp + forceDirectional, ForceMode.Impulse);
    }

    public  void MapInputs()
    {
        if (Input.GetButtonDown(Constants.Input.UseAbility))
        {
            JoystickValues joystickValues = new JoystickValues()
            {
                X = Input.GetAxis(Constants.Input.TurnAxis),
                Y = Input.GetAxis(Constants.Input.UpAndDownAxis)
            };
            UseAbility(joystickValues);
        }
    }

    // PRIVATE

    private void UseAbility(JoystickValues joystickValues)
    {
        if (_canUseAbility)
        {
            if (!_hasDoneFirstJump)
            {
                FirstJump();
            }
            else
            {
                SecondJump(joystickValues);
                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown ()
    {
        _canUseAbility = false;
        yield return new WaitForSeconds(Settings.CooldownDuration);
        _canUseAbility = true;
    }
}
