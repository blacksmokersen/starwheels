using UnityEngine;
using UnityEngine.UI;
using Bolt;

namespace Common.HUD
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class KillFeed : GlobalEventListener
    {
        [Header("Entry Settings")]
        [SerializeField] private float _entryLifespan = 5f;

        [Header("Entry")]
        [SerializeField] private GameObject _entryPrefab;

        private VerticalLayoutGroup _killFeedLayout;
        private Items.ItemListData _itemListData;

        private void Awake()
        {
            _killFeedLayout = GetComponent<VerticalLayoutGroup>();
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
            var entry = Instantiate(_entryPrefab).GetComponent<KillFeedEntry>();
            entry.SetKillerNameAndColor(evnt.KillerName, evnt.KillerTeamColor);
            entry.SetItemIcon(_itemListData.GetItemIconUsingName(evnt.Item));
            entry.SetVictimNameAndColor(evnt.VictimName, evnt.VictimTeamColor);
            entry.transform.SetParent(transform);

            Destroy(entry, _entryLifespan);
        }
    }
}
