using UnityEngine;

namespace Refacto
{
    [CreateAssetMenu]
    public class Ability : ScriptableObject
    {
        public Effect Effect;
        public float Cooldown;
    }
}
