using UnityEngine;

namespace Items
{
    public class DiskRotation : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(new Vector3(0, 5, 0));
        }
    }
}
