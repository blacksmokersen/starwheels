using UnityEngine;
using Bolt;

namespace Multiplayer.Teams
{
    public class KartColorSetter : EntityBehaviour<IKartState>
    {
        [SerializeField] private Renderer targetKartRenderer;

        private PlayerSettings _playerSettings;

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        public override void Attached()
        {
            targetKartRenderer.material.SetColor("_BaseColor", state.Team);
        }
    }
}
