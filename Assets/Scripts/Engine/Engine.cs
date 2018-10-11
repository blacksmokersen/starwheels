using UnityEngine;

namespace Engine
{
    public class Engine : Bolt.EntityBehaviour<IKartState>, IControllable
    {
        [Header("Forces")]
        public EngineSettings Settings;

        [Header("Events")]
        public FloatEvent OnVelocityChange;

        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            ClampMagnitude();
            OnVelocityChange.Invoke(_rb.velocity.magnitude);
        }

        private void ClampMagnitude()
        {
            if (Settings.MaxMagnitude > 0)
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, Settings.MaxMagnitude);
        }

        // PUBLIC

        public void MapInputs()
        {
            Accelerate(Input.GetAxis(Constants.Input.Accelerate));
            Decelerate(Input.GetAxis(Constants.Input.Decelerate));
        }

        public override void SimulateOwner()
        {
            MapInputs();
        }

        // PRIVATE

        private void Accelerate(float value)
        {
            _rb.AddRelativeForce(Vector3.forward * value * Settings.SpeedForce, ForceMode.Force);
        }

        private void Decelerate(float value)
        {
            _rb.AddRelativeForce(Vector3.back * value * Settings.SpeedForce / Settings.DecelerationFactor, ForceMode.Force);
        }
    }
}
