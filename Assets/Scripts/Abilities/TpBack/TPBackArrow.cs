using UnityEngine;
using Items;

namespace Abilities
{
    public class TPBackArrow : MonoBehaviour
    {
        [SerializeField] private Renderer _arrowTotem;

        private TPBackAbility _tpBackAbility;
        private TPBackBehaviour _tpBackBehaviour;

        // CORE

        private void Awake()
        {
            _tpBackAbility = GetComponentInParent<TPBackAbility>();
        }

        private void OnEnable()
        {
            _tpBackBehaviour = _tpBackAbility.Dislocator.GetComponent<TPBackBehaviour>();
        }

        private void Update()
        {
            FollowDislocator();
            ArrowColorSetter();
        }

        //PRIVATE

        private void FollowDislocator()
        {
            if (_tpBackAbility != null)
                this.transform.forward = new Vector3(-(_tpBackAbility.Dislocator.transform.position.x - this.transform.position.x), 0, -(_tpBackAbility.Dislocator.transform.position.z - this.transform.position.z));
            if (_tpBackAbility.Dislocator == null)
            {
                Debug.LogError("Lose DislocatorTransform for Arrow Direction : Need Fix");
                gameObject.SetActive(false);
            }
        }

        private void ArrowColorSetter()
        {
            if (_tpBackBehaviour != null)
            {
                if ((_tpBackBehaviour.ActualDistance / _tpBackBehaviour.MaxDistance) * 100 <= 50)
                    _arrowTotem.material.SetColor("_Color", Color.green);
                else if ((_tpBackBehaviour.ActualDistance / _tpBackBehaviour.MaxDistance) * 100 <= 85)
                    _arrowTotem.material.SetColor("_Color", Color.yellow);
                else if ((_tpBackBehaviour.ActualDistance / _tpBackBehaviour.MaxDistance) * 100 > 85)
                    _arrowTotem.material.SetColor("_Color", Color.red);
            }
        }
    }
}
