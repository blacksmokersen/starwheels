using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kart;


public class KartMeshMovement : MonoBehaviour
{
    private float MaxMeshAngleDrift;

    private float wheelsSpeed;

    public GameObject frontWheelLeft;
    public GameObject frontWheelRight;
    public GameObject backWheelsL;
    public GameObject backWheelsR;
    public GameObject kartModel;

    public void FrontWheelsMovement(float currentAngle,float playerVelocity)
    {
        wheelsSpeed = -playerVelocity;
        if (playerVelocity > 0.01f)
        {
            // n'est pas pris en compte car le transform en dessous force la rotation à revoir
            frontWheelLeft.transform.Rotate(Vector3.right * wheelsSpeed);
            frontWheelRight.transform.Rotate(Vector3.right * wheelsSpeed);
        }
        frontWheelLeft.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
        frontWheelRight.transform.localEulerAngles = new Vector3(0, currentAngle * 30, 0);
    }

    public void BackWheelsMovement(float playerVelocity)
    {
        wheelsSpeed = playerVelocity;
        if (playerVelocity > 0.01f)
        {
            backWheelsL.transform.Rotate(Vector3.right * wheelsSpeed);
            backWheelsR.transform.Rotate(Vector3.right * wheelsSpeed);
        }
    }
}

