using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Options
{
    public class BubbleEffectsGestion : MonoBehaviour
    {
        [SerializeField] private Transform[] _bubles;
        private Vector3 _destination;

        [SerializeField] private float _bubbleSpeed = 3.0f;

        private void OnEnable()
        {
            foreach (Transform _buble in _bubles)
            {
                _buble.position = transform.position;
            }
        }

        private void Update()
        {
            GetDirection();
            _bubles[0].transform.position += _destination * _bubbleSpeed * Time.deltaTime;
            AllBublePosition();
        }

        private void GetDirection()
        {
            _destination = Input.mousePosition - _bubles[0].position;
        }

        private void AllBublePosition()
        {
            for (int i = 1; i < _bubles.Length; i++)
            {
                _bubles[i].position = _bubles[0].position + ((float)(i + 1) / _bubles.Length) * (2 *(transform.position - _bubles[0].position)); // Each buble is placed on equal distance form each over on symmetry line which center is the buttons center and one extremity is the first buble
            }
        }
    }
}