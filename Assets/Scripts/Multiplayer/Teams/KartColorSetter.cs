using UnityEngine;

namespace Multiplayer.Teams
{
    public class KartColorSetter : MonoBehaviour
    {
        [SerializeField] private Renderer targetKartRenderer;

        private Material _redKartMaterial;
        private Material _blueKartMaterial;
        private Material _whiteKartMaterial;

        private void Awake()
        {
            _redKartMaterial = Resources.Load<Material>(Constants.Materials.RedKart);
            _blueKartMaterial = Resources.Load<Material>(Constants.Materials.BlueKart);
            _whiteKartMaterial = Resources.Load<Material>(Constants.Materials.WhiteKart);
        }

        // PUBLIC

        public void SetKartColorUsingTeam(Team team)
        {
            switch (team)
            {
                case Team.Blue:
                    targetKartRenderer.material = _blueKartMaterial;
                    break;
                case Team.Red:
                    targetKartRenderer.material = _redKartMaterial;
                    break;
                case Team.None:
                    targetKartRenderer.material = _whiteKartMaterial;
                    break;
            }
        }

        public void SetKartColor(Color color)
        {
            if(color == TeamsColors.BlueColor)
                targetKartRenderer.material = _blueKartMaterial;
            else if (color == TeamsColors.RedColor)
                targetKartRenderer.material = _redKartMaterial;
            else
                targetKartRenderer.material = _whiteKartMaterial;
        }
    }
}
