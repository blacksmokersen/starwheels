using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Customization
{
    public class KartMeshDisabler : MonoBehaviour
    {
        [SerializeField] GameObject[] _kartNamePlate;

        private List<GameObject> _currentKartGraphics;

        //CORE

        private void Awake()
        {
            _currentKartGraphics = new List<GameObject>();
        }

        //PUBLIC

        public void EnableKartMeshes(bool enableNameplate)
        {
            if (enableNameplate)
            {
                foreach (GameObject mesh in _kartNamePlate)
                {
                    mesh.SetActive(true);
                }
            }
            foreach (GameObject child in _currentKartGraphics)
            {
                child.gameObject.SetActive(true);
            }
            ClearCurrentKartGraphics();
        }

        public void DisableKartMeshes(bool disableNameplate)
        {
            var i = 0;

            if (disableNameplate)
            {
                foreach (GameObject mesh in _kartNamePlate)
                {
                    mesh.SetActive(false);
                }
            }
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    _currentKartGraphics.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                    i++;
                }
            }
        }

        public void DisableKartMeshesForXSeconds(float duration)
        {
            StartCoroutine(DisableKartMeshRoutine(duration));
        }

        //PRIVATE

        private void ClearCurrentKartGraphics()
        {
            _currentKartGraphics.Clear();
        }

        IEnumerator DisableKartMeshRoutine(float duration)
        {
            DisableKartMeshes(true);
            yield return new WaitForSeconds(duration);
            EnableKartMeshes(true);
        }
    }
}
