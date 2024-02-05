using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyKilledEventArgs : EventArgs
{
    public GameObject KilledEnemy { get; set; }
    public EnemyKilledEventArgs(GameObject enemy)
    {
        KilledEnemy = enemy;
    }

}