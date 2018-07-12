using System.Collections;
using UnityEngine;

namespace Kart
{
    /*
     * Class for handling the Kart orientation (drift, turn etc.)
     */
    public class KartOrientation : MonoBehaviour
    {
        private KartStates kartStates;

        [Header("Turn")]
        public float TurningSpeed;

        [Header("Drift")]
        public float DriftingTurningSpeed;
        [Range(0, 1)] public float DriftMaxAngle;
        [Range(0, 1)] public float DriftMinAngle;

        public float RotationStabilizationSpeed;

        // Drift Wydman
        public float DriftTurnSpeed = 10;
        public float DriftTurnContrainte = 30f;
        public float MaxExteriorAngle = 0.05f;
        public bool BalancingDriftL = false;
        public bool BalancingDriftR = false;
        public bool IsCoroutineStarted = false;
        public bool Crash;

        private float balanceDriftL = 0;
        private float balanceDriftR = 0;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
        }

        private void FixedUpdate()
        {
            StabilizeRotation();
        }

        //  A VIRER  ? en parler à shashimee
        public void Turn(float value)
        {
            Debug.Log("Turn dans Kart.Orientation");
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                //   transform.Rotate(new Vector3(0, value * TurningSpeed * Time.deltaTime, 0));
            }
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight || kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                //   transform.Rotate(new Vector3(0, value * DriftingTurningSpeed * Time.deltaTime, 0));
            }
        }

        public void DriftTurn(float angle)
        {

            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                if (BalancingDriftL && kartStates.DriftBoostState == DriftBoostStates.SimpleDrift)
                {
                    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
                    {
                        balanceDriftL = Mathf.Lerp(balanceDriftL, -0.8f, t);
                    }
                }
                if (!IsCoroutineStarted)
                {
                    StartCoroutine(BalanceDriftL(2f));
                }
                if (angle != 0)
                {
                    angle = Mathf.Clamp(angle, -0.8f, -MaxExteriorAngle + balanceDriftL);
                    transform.Rotate(Vector3.up * DriftTurnContrainte * angle / 2 * DriftTurnSpeed * Time.deltaTime);
                }
                else
                {
                    angle = Mathf.Clamp(angle, -0.8f, -0.2f + balanceDriftL);
                    transform.Rotate(Vector3.up * DriftTurnContrainte * angle / 2 * DriftTurnSpeed * Time.deltaTime);
                }
                //   transform.Rotate(new Vector3(0, Mathf.Clamp(angle, -DriftMaxAngle, -DriftMinAngle) * DriftingTurningSpeed * Time.deltaTime, 0));
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                if (BalancingDriftR && kartStates.DriftBoostState == DriftBoostStates.SimpleDrift)
                {
                    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
                    {
                        balanceDriftR = Mathf.Lerp(balanceDriftR, 0.8f, t);
                    }
                }
                if (!IsCoroutineStarted)
                {
                    StartCoroutine(BalanceDriftR(2f));
                }
                if (angle != 0)
                {
                    angle = Mathf.Clamp(angle, MaxExteriorAngle + balanceDriftR, 0.8f);
                    transform.Rotate(Vector3.up * DriftTurnContrainte * angle / 2 * DriftTurnSpeed * Time.deltaTime);
                }
                else
                {
                    angle = Mathf.Clamp(angle, 0.2f + balanceDriftR, 0.8f);
                    transform.Rotate(Vector3.up * DriftTurnContrainte * angle / 2 * DriftTurnSpeed * Time.deltaTime);
                }
                //  transform.Rotate(new Vector3(0, Mathf.Clamp(angle, DriftMinAngle, DriftMaxAngle) * DriftingTurningSpeed * Time.deltaTime, 0));
            }

        }

        public void NotDrifting()
        {
            BalancingDriftR = false;
            BalancingDriftL = false;
            balanceDriftL = 0;
            balanceDriftR = 0;
            IsCoroutineStarted = false;
        }

        public void StabilizeRotation()
        {
            if (kartStates.AirState == AirStates.InAir)
            {
                var actualRotation = transform.localRotation;
                actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed);
                actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed);
                transform.localRotation = actualRotation;
            }
        }

        public void LooseHealth(float crashTimer)
        {
            float random = Random.Range(0, 1);
            if (random >= 0.5f)
            {
                StartCoroutine(CrashBehaviour(crashTimer));
            }
            else
            {
                StartCoroutine(CrashBehaviour(crashTimer));
            }
        }

        IEnumerator BalanceDriftL(float balanceTiming)
        {
            IsCoroutineStarted = true;

            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                yield return new WaitForSeconds(balanceTiming);
                BalancingDriftL = true;
            }
        }

        IEnumerator BalanceDriftR(float balanceTiming)
        {
            IsCoroutineStarted = true;

            if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                yield return new WaitForSeconds(balanceTiming);
                BalancingDriftR = true;
            }
        }

        IEnumerator CrashBehaviour(float crashTimer)
        {
            Crash = true;
            yield return new WaitForSeconds(crashTimer);
            Crash = false;
        }
    }
}