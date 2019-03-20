using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Photon.Lobby
{
    public class PopulateMapDropdown : MonoBehaviour
    {
        [SerializeField] private MapListData _mapListData;
        [SerializeField] private TMP_Dropdown _mapDropdown;

        private void Awake()
        {
            var options = new List<string>();

            foreach(var map in _mapListData.MapList)
            {
                options.Add(map.MapName);
            }

            _mapDropdown.AddOptions(options);
        }
    }
}
