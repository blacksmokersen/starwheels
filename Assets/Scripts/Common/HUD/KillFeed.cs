using UnityEngine;
using UnityEngine.UI;
using Bolt;
using Items;
using Multiplayer;

namespace Common.HUD
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class KillFeed : GlobalEventListener
    {
        [Header("Entry")]
        [SerializeField] private GameObject _entryPrefab;

        private ItemListData _itemListData;
        private PlayerSettings _playerSettings;

        private void Awake()
        {
            _itemListData = Resources.Load<ItemListData>(Constants.Resources.ItemListData);
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
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

            entry.SetKillerNameAndColor(evnt.KillerName, evnt.KillerTeamColor, evnt.KillerName == _playerSettings.Nickname);
            entry.SetItemIcon(_itemListData.GetItemIconUsingName(evnt.Item));
            entry.SetVictimNameAndColor(evnt.VictimName, evnt.VictimTeamColor, evnt.VictimName == _playerSettings.Nickname);
            entry.transform.localPosition = Vector3.zero;

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}
