using UnityEngine;

namespace Common.Graphics
{
    public class ShaderVariablesUpdater : MonoBehaviour
    {
        [Header("Material")]
        [SerializeField] private Material _materialInstance;

        [Header("Shader Settings")]
        [SerializeField] private string _shaderVariableName;
        [SerializeField] private float _initialValue;

        // CORE

        private void Awake()
        {
            _materialInstance.SetFloat(Shader.PropertyToID(_shaderVariableName), _initialValue);
        }

        // PUBLIC

        public void UpdateShaderVariable(float newValue)
        {
            _materialInstance.SetFloat(Shader.PropertyToID(_shaderVariableName), newValue);
        }
    }
}
