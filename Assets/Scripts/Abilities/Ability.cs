using UnityEngine;

namespace Abilities
{
    public class Ability : BaseKartComponent
    {
        // CORE

        protected new void Awake()
        {
            base.Awake();
        }

        // PUBLIC

        public virtual void Use(float xAxis, float yAxis) { }

        // PRIVATE
    }
}
