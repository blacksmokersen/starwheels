using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameModes.Totem
{

    public class ArrowTotem : MonoBehaviour
    {
        [SerializeField] private TotemPossession _totemPossession;

        private Transform _totemPosition;
        private Renderer _arrowTotem;

        // Start is called before the first frame update
        void Start()
        {
            _totemPosition = GameObject.Find("Totem(Clone)").GetComponent<Transform>();
            _arrowTotem = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            // Inverted lookAt
            this.transform.LookAt(2 * transform.position - _totemPosition.position);

            if (_totemPossession._isLocalOwner)
            {
                _arrowTotem.enabled = false;
            }
            else 
            {
                _arrowTotem.enabled = true;
            }
        }
    }
}