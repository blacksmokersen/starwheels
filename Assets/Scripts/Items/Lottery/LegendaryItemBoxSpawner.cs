using System.Collections;
using UnityEngine;

namespace Items.Lottery
{
    [CreateAssetMenu(menuName = "Item Settings/Legendary Upgrade")]
    public class LegendaryItemBoxSpawner : MonoBehaviour
    {
        [SerializeField] private LegendaryItemBoxesSpawnerSettings _settings;

        private ItemBox[] _itemBoxes;

        private void Start()
        {
            _itemBoxes = FindObjectsOfType<ItemBox>();

            if (_settings.UpgradeEveryXSeconds)
            {
                StartCoroutine(UpgradeOneItemBoxRoutine());
            }
        }

        private void UpgradeOneItemBox()
        {
            Debug.Log("Upgrading one");
            _itemBoxes[Random.Range(0, _itemBoxes.Length)].UpgradeToNext();
        }

        private void ResetAllItemBoxes()
        {
            foreach (var itemBox in _itemBoxes)
            {
                itemBox.ResetToFirstUpgrade();
            }
        }

        private IEnumerator UpgradeOneItemBoxRoutine()
        {
            while (_settings.UpgradeEveryXSeconds)
            {
                yield return new WaitForSeconds(_settings.SecondsBetweenUpgrades);
                ResetAllItemBoxes();
                UpgradeOneItemBox();
            }
        }
    }
}
