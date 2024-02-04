using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TowerSelectionArgs : EventArgs
{
    public GameObject TowerPrefab { get; set; }

    public TowerSelectionArgs(GameObject _towerPrefab)
    {
        TowerPrefab = _towerPrefab;
    }
}