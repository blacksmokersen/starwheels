using System.Collections;
using UnityEngine;

namespace MapsSpecifics
{
    [DisallowMultipleComponent]
    public class TotemTeleporter : MonoBehaviour
    {
        [Tooltip("The other teleporter that represents the exit of this one.")]
        [SerializeField] private TotemTeleporter _otherTeleporter;

        public bool Enabled = true;

        // CORE

        private void OnTriggerEnter(Collider other)
        {
            if (Enabled && other.CompareTag(Constants.Tag.TotemRespawn))
            {
                _otherTeleporter.DisableForXSeconds(1f);

                var totemRoot = other.GetComponentInParent<BoltEntity>().gameObject;
                totemRoot.transform.position = _otherTeleporter.transform.position;
            }
        }

        // PUBLIC

        public void DisableForXSeconds(float x)
        {
            StartCoroutine(DisableForXSecondsRoutine(x));
        }

        // PRIVATE

        private IEnumerator DisableForXSecondsRoutine(float x)
        {
            Enabled = false;
            yield return new WaitForSeconds(x);
            Enabled = true;
        }
    }
}
