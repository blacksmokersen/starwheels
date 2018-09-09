using UnityEngine;

/*
 * Generic class to have a continuious rotating game object.
 * (Used mainly for rotating the loading image)
 */
public class Rotator : MonoBehaviour {

    [SerializeField] private GameObject thingToRotate;
    [SerializeField] private float speed;

    // CORE

    private void Awake()
    {
        if (thingToRotate == null)
        {
            thingToRotate = gameObject;
        }
    }

    private void Update () {
        thingToRotate.transform.Rotate(0f, 0f, speed * Time.deltaTime);
	}

    // PUBLIC

    // PRIVATE
}
