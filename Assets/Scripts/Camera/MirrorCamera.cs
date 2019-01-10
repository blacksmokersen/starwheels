using UnityEngine;

namespace CameraUtils
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class MirrorCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void OnPreCull()
        {
            _camera.ResetWorldToCameraMatrix();
            _camera.ResetProjectionMatrix();
            _camera.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        }

        private void OnPreRender()
        {
            GL.invertCulling = true;
        }

        private void OnPostRender()
        {
            GL.invertCulling = false;
        }
    }
}
