using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "TowerScriptableObject", menuName = "ScriptableObject/TowerSO")]
public class TowerSO : ScriptableObject
{
    public GameObject towerPrefab;
    public Sprite towerIcon;
}