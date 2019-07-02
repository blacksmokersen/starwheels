using UnityEngine;

namespace Items
{
    [DisallowMultipleComponent]
    public class ItemDisplayerShieldNode : MonoBehaviour
    {
        public ItemRarity Rarity;

        [Header("Objects to Display")]
        [SerializeField] private GameObject _frontObject;
        [SerializeField] private GameObject _backObject;

        // PUBLIC

        public void Display(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    _frontObject.SetActive(true);
                    _backObject.SetActive(false);
                    break;
                case Direction.Backward:
                    _frontObject.SetActive(false);
                    _backObject.SetActive(true);
                    break;
                default:
                    _frontObject.SetActive(true);
                    _backObject.SetActive(false);
                    break;
            }
        }

        public void Hide()
        {
            _frontObject.SetActive(false);
            _backObject.SetActive(false);
        }
    }
}
