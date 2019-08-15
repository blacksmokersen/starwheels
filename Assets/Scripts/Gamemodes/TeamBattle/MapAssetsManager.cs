using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAssetsManager : MonoBehaviour
{
    [SerializeField] private GameObject _redJailDoor;
    [SerializeField] private GameObject _blueJailDoor;

    [SerializeField] private GameObject _redButton;
    [SerializeField] private GameObject _blueButton;

    [SerializeField] private GameObject[] _TeamBattleAssetsToEnable;
    [SerializeField] private GameObject[] _BaseMapAssetsToDisable;

}
