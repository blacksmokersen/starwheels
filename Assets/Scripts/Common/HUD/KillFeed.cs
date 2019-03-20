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
        private GameSettings _gameSettings;

        private void Awake()
        {
            _itemListData = Resources.Load<ItemListData>(Constants.Resources.ItemListData);
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
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

            var killerColor = _gameSettings.TeamsListSettings.GetSettings(evnt.KillerTeam.ToTeam()).KillFeedEntryColor;
            entry.SetKillerNameAndColor(evnt.KillerName, killerColor, evnt.KillerName == _playerSettings.Nickname);
            entry.SetItemIcon(_itemListData.GetItemIconUsingName(evnt.Item));
            var victimrColor = _gameSettings.TeamsListSettings.GetSettings(evnt.VictimTeam.ToTeam()).KillFeedEntryColor;
            entry.SetVictimNameAndColor(evnt.VictimName, victimrColor, evnt.VictimName == _playerSettings.Nickname);
            entry.transform.localPosition = Vector3.zero;

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}
