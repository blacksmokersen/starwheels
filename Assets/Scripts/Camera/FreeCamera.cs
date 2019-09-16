using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;

namespace CameraUtils
{
    public class FreeCamera : MonoBehaviour, IControllable, IObserver
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [SerializeField] private bool _switchInputs;

        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _verticalSpeed;
        [SerializeField] private float _rotateYSpeed;
        [SerializeField] private float _rotateXSpeed;
        [SerializeField] private float _forwardSpeed;
        [SerializeField] private float _backwardSpeed;

        private GameObject _kart;

        //CORE

        private void Update()
        {
            MapInputs();
        }

        //PUBLIC

        public void Observe(GameObject kartroot)
        {
            _kart = kartroot;
        }

        public void MapInputs()
        {
            if (Enabled)
            {
                transform.eulerAngles += new Vector3(0, Input.GetAxis(Constants.Input.TurnCamera) * _rotateXSpeed, 0);
                transform.eulerAngles += new Vector3(-Input.GetAxis(Constants.Input.UpAndDownCamera) * _rotateYSpeed, 0, 0);
                if (Input.GetAxis(Constants.Input.TurnCamera) == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        transform.eulerAngles += new Vector3(0, - _rotateXSpeed, 0);
                    }
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        transform.eulerAngles += new Vector3(0, _rotateXSpeed, 0);
                    }
                }


                if (_switchInputs)
                {
                    transform.Translate(Vector3.forward * Input.GetAxis(Constants.Input.Accelerate) * _forwardSpeed);
                    transform.Translate(Vector3.back * Input.GetAxis(Constants.Input.Decelerate) * _backwardSpeed);
                    transform.Translate(Vector3.right * Input.GetAxis(Constants.Input.TurnAxis) * _horizontalSpeed);
                    transform.Translate(Vector3.up * Input.GetAxis(Constants.Input.UpAndDownAxis) * _verticalSpeed);
                }
                else
                {
                    transform.Translate(Vector3.forward * Input.GetAxis(Constants.Input.UpAndDownAxis) * _forwardSpeed);
                    transform.Translate(Vector3.right * Input.GetAxis(Constants.Input.TurnAxis) * _horizontalSpeed);
                    //transform.Translate(Vector3.up * Input.GetAxis(Constants.Input.Accelerate) * _verticalSpeed);
                    transform.Translate(Vector3.down * Input.GetAxis(Constants.Input.Triggers) * _verticalSpeed);

                    if (Input.GetAxis(Constants.Input.UpAndDownAxis) == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            transform.Translate(Vector3.forward * _forwardSpeed);
                        }
                        if (Input.GetKeyDown(KeyCode.S))
                        {
                            transform.Translate(Vector3.back * _forwardSpeed);
                        }
                    }
                    if (Input.GetAxis(Constants.Input.TurnAxis) == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            transform.Translate(Vector3.left * _forwardSpeed);
                        }
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            transform.Translate(Vector3.right * _forwardSpeed);
                        }
                    }

                    if (Input.GetAxis(Constants.Input.Accelerate) == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            transform.Translate(Vector3.up * _verticalSpeed);
                        }
                    }

                    if (Input.GetAxis(Constants.Input.Decelerate) == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftShift))
                        {
                            transform.Translate(Vector3.down * _verticalSpeed);
                        }
                    }
                }
            }
        }

        public void DisableKartControls()
        {
            _kart.GetComponentInChildren<EngineBehaviour>().Enabled = false;
        }
        public void EnableKartControls()
        {
            _kart.GetComponentInChildren<EngineBehaviour>().Enabled = true;
        }
    }
}
