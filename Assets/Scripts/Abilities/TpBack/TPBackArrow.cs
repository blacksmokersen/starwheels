using UnityEngine;
using Items;

namespace Abilities
{
    public class TPBackArrow : MonoBehaviour
    {
        [SerializeField] private Renderer _arrowTotem;

        private TPBackAbility _tpBackAbility;

        // CORE

        private void Awake()
        {
            _tpBackAbility = GetComponentInParent<TPBackAbility>();
        }

        private void Update()
        {
            if (_tpBackAbility != null)
                this.transform.forward = new Vector3(-(_tpBackAbility.DislocatorTransform.transform.position.x - this.transform.position.x), 0, -(_tpBackAbility.DislocatorTransform.transform.position.z - this.transform.position.z));
        }
    }
}
