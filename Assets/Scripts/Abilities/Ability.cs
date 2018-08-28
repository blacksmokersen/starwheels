using UnityEngine;

namespace Abilities
{
    public class Ability : BaseKartComponent
    {
        private float _energy = 1f;
        public float Energy
        {
            get { return _energy; }
            set
            {
                _energy = Mathf.Clamp(value, 0f, 1f);
                kartEvents.OnEnergyConsumption(_energy);
            }
        }

        protected new void Awake()
        {
            base.Awake();
            //kartEvents.OnCollisionEnterItemBox += () => Energy = 1f;
        }

        public virtual void Use(float xAxis, float yAxis) { }
    }
}
