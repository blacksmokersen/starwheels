using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMeshDisabler : MonoBehaviour
{
    [SerializeField] GameObject[] _kartMeshes;
    [SerializeField] GameObject[] _kartNamePlate;

    public void EnableKartMeshes(bool enableNameplate)
    {
        if (enableNameplate)
        {
            foreach (GameObject mesh in _kartNamePlate)
            {
                mesh.SetActive(true);
            }
        }
        foreach (GameObject mesh in _kartMeshes)
        {
            mesh.SetActive(true);
        }
    }

    public void DisableKartMeshes(bool disableNameplate)
    {
        if (disableNameplate)
        {
            foreach (GameObject mesh in _kartNamePlate)
            {
                mesh.SetActive(false);
            }
        }
        foreach (GameObject mesh in _kartMeshes)
        {
            mesh.SetActive(false);
        }
    }

    public void DisableKartMeshesForXSeconds(float duration)
    {
        StartCoroutine(DisableKartMeshRoutine(duration));
    }

    IEnumerator DisableKartMeshRoutine(float duration)
    {
        DisableKartMeshes(true);
        yield return new WaitForSeconds(duration);
        EnableKartMeshes(true);
    }
}
