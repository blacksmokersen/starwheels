using UnityEngine;
using UnityEngine.UI;
using Bolt;

namespace Common.HUD
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class KillFeed : GlobalEventListener
    {
        [Header("Entry")]
        [SerializeField] private GameObject _entryPrefab;

        private Items.ItemListData _itemListData;

        private void Awake()
        {
            _itemListData = Resources.Load<Items.ItemListData>(Constants.Resources.ItemListData);
        }

        // BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            CreateEntry(evnt);
        }

        // PRIVATE

        private void CreateEntry(PlayerHit evnt)
        {
            var entry = Instantiate(_entryPrefab, transform, false).GetComponent<KillFeedEntry>();

            entry.SetKillerNameAndColor(evnt.KillerName, evnt.KillerTeamColor);
            entry.SetItemIcon(_itemListData.GetItemIconUsingName(evnt.Item));
            entry.SetVictimNameAndColor(evnt.VictimName, evnt.VictimTeamColor);
            entry.transform.localPosition = Vector3.zero;

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}
