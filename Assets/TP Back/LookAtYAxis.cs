using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtYAxis : MonoBehaviour {

    public GameObject target;


	// Use this for initialization

	
	// Update is called once per frame
	void Update () {

        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(targetPosition);
		
	}
}
