using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kart;


public class KartMeshMovement : MonoBehaviour
{
    public float MaxMeshAngleDrift;

    private float wheelsSpeed;
    private float currentAngle;

    public GameObject frontWheelLeft;
    public GameObject frontWheelRight;
    public GameObject backWheelsL;
    public GameObject backWheelsR;
    public GameObject kartModel;

    private KartStates kartStates;
    private KartPhysics kartPhysics;

    private void Awake()
    {
        kartStates = FindObjectOfType<KartStates>();
        kartPhysics = FindObjectOfType<KartPhysics>();
    }

    private void Update()
    {
        KartModelMovement();
        FrontWheelsMovement(kartPhysics.PlayerVelocity);
        BackWheelsMovement(kartPhysics.PlayerVelocity);
    }

    public void KartModelMovement()
    {
        currentAngle = Input.GetAxis(Constants.TurnAxis);
        MaxMeshAngleDrift = Mathf.Clamp(currentAngle, -0.05f, 0.05f);
        if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
        {
            // rotation du mesh du kart
            // en gros les angles sont inversé, je voulais que z ne dépasse pas 5( affiché sur l'inspecteur pour l'objet
            // mais en gros l'équivalent par la console en game est 355, ensuite je rajoute le || = 0 car l'angle commence a 0.
            if (kartModel.transform.localEulerAngles.z >= 355 || kartModel.transform.localEulerAngles.z == 0)
            {
                kartModel.transform.localEulerAngles += new Vector3(0, 0, MaxMeshAngleDrift);
            }
        }
        else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
        {
            if (kartModel.transform.localEulerAngles.z <= 5 || kartModel.transform.localEulerAngles.z == 0)
            {
                kartModel.transform.localEulerAngles += new Vector3(0, 0, MaxMeshAngleDrift);
            }
        }
        else if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
        {
           // kartModel.transform.localEulerAngles = new Vector3(kartModel.transform.localEulerAngles.x, kartModel.transform.localEulerAngles.y, 0);
        }
    }

    public void FrontWheelsMovement(float playerVelocity)
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

