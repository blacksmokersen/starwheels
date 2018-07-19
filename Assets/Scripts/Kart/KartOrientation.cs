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
        [Range(1, 3)] public float LowerTurnSensitivity;

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

        private Coroutine c1;
        private Coroutine c2;


        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
        }

        private void FixedUpdate()
        {
            StabilizeRotation();
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
                    c1 = StartCoroutine(BalanceDrift(2f, "Left"));
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
                    c2 = StartCoroutine(BalanceDrift(2f, "Right"));
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
            if (c1 != null) StopCoroutine(c1);
            if (c2 != null) StopCoroutine(c2);
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

        // si on décide d'ajouter un effet sur l'orientation lorsque le joueur prends un dégat
        // l'aspect random sera là pour donner une trajectoire semi aléatoire pour que le joueur ne puisse pas jouer avec l'effet de l'impact
        // pas dit que ce soit utilisé un jour
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

        IEnumerator BalanceDrift(float balanceTiming, string Direction)
        {
            IsCoroutineStarted = true;
            if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting && kartStates.DriftBoostState == DriftBoostStates.SimpleDrift)
            {
                yield return new WaitForSeconds(balanceTiming);
                if (kartStates.DriftBoostState == DriftBoostStates.SimpleDrift && Direction == "Left")
                {
                    BalancingDriftL = true;
                }
                else if (kartStates.DriftBoostState == DriftBoostStates.SimpleDrift && Direction == "Right")
                {
                    BalancingDriftR = true;
                }
                else
                {
                    if (c1 != null) StopCoroutine(c1);
                    if (c2 != null) StopCoroutine(c2);
                }
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