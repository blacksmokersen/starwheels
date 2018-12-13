using UnityEngine;
using UnityEngine.UI;
using Bolt;

namespace Common.HUD
{
    public class KillFeed : GlobalEventListener
    {
        [Header("Layout")]
        [SerializeField] private VerticalLayoutGroup _killFeedLayout;

        [Header("Entry")]
        [SerializeField] private GameObject _entryPrefab;

        // BOLT

        public override void OnEvent(KartDestroyed evnt)
        {
            CreateEntry("Hello", "world", "bitch");
        }

        // PRIVATE

        private void CreateEntry(string killerName, string item, string victimName)
        {
            var entry = Instantiate(_entryPrefab).GetComponent<KillFeedEntry>();
            entry.SetKillerName(killerName);
            entry.SetVictimName(victimName);
            _killFeedLayout.transform.SetAsFirstSibling();
        }
    }
}
