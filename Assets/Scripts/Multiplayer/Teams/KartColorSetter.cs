using UnityEngine;
using Bolt;

namespace Multiplayer.Teams
{
    public class KartColorSetter : EntityBehaviour<IKartState>
    {
        [SerializeField] private Renderer targetKartRenderer;

        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        // BOLT

        public override void Attached()
        {
            state.AddCallback("Team", TeamChanged);
        }

        // PRIVATE

        private void TeamChanged()
        {
            Debug.Log("Team changed : " + state.Team);
            targetKartRenderer.material.SetColor("_BaseColor", state.Team);
        }
    }
}
