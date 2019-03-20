using UnityEngine;
using Bolt;
using System.Collections;
using Multiplayer.Teams;

namespace Gamemodes.Totem
{
    public class TotemColorChanger : GlobalEventListener
    {
        [Header("Possession")]
        public Team CurrentTeam;

        [Header("Events")]
        public ColorEvent OnColorChange;

        [SerializeField] private Renderer _totemAuraRenderer;

        private Color _defaultColor;
        private TeamsListSettings _teamsList;

        // CORE

        private void Awake()
        {
            _teamsList = Resources.Load<GameSettings>(Constants.Resources.GameSettings).TeamsListSettings;

            _defaultColor = _totemAuraRenderer.material.color;
            CurrentTeam = Team.None;
        }

        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            CurrentTeam = evnt.Team.ToTeam();
            ChangeColor(_teamsList.GetSettings(evnt.Team.ToTeam()).ItemsColor);
            StartCoroutine(SecurityRoutine());
        }

        public override void OnEvent(TotemPicked evnt)
        {
            ResetToDefault();
        }

        // PUBLIC

        public void ChangeColor(Color c)
        {
            _totemAuraRenderer.material.color = c;

            if (OnColorChange != null)
            {
                OnColorChange.Invoke(c);
            }
        }

        public void ResetToDefault()
        {
            CurrentTeam = Team.None;
            ChangeColor(_defaultColor);
            StopAllCoroutines();
        }

        public bool IsDefault()
        {
            return CurrentTeam == Team.None;
        }

        // PRIVATE

        private IEnumerator SecurityRoutine()
        {
            yield return new WaitForSeconds(20f);
            if (!IsDefault())
            {
                ResetToDefault();
            }
        }
    }
}
