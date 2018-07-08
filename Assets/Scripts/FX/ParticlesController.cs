using UnityEngine;

namespace FX {
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlesController : MonoBehaviour
    {
        ParticleSystem.MainModule myParticleSystem;

        private void Awake()
        {
            myParticleSystem = GetComponent<ParticleSystem>().main;
        }

        public void SetColor(Color color)
        {
            var main = myParticleSystem;
            main.startColor = color;
        }

        public void Hide()
        {
            var color = myParticleSystem.startColor.color;
            color.a = 0f;
            myParticleSystem.startColor = color;
        }

        public void Show()
        {
            var color = myParticleSystem.startColor.color;
            color.a = 1f;
            myParticleSystem.startColor = color;
        }
    }
}
