using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : MonoBehaviour
    {
        public ThrowableType ThrowableType;
    }
}
