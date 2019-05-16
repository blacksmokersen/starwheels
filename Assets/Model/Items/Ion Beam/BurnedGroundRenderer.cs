using UnityEngine;

public class BurnedGroundRenderer : MonoBehaviour
{
    [Range(0, 1)] public float Opacity;
    [Range(0, 10)] public float BurnedDispersion;

    private Material _mat;

    private void Awake()
    {
        _mat = GetComponent<Projector>().material;
    }

    private void Update()
    {
        _mat.SetFloat("_Opacity", Opacity);
        _mat.SetFloat("_BurnedDispertion", BurnedDispersion);
    }
}
