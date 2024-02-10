using UnityEngine;
using System;


public class SpawnPointEventArgs : EventArgs
{
    public GameObject EnemyPrefab { get; set; }
    public int WaveNumber { get; set; }
    public SpawnPointEventArgs(GameObject prefab, int waveNumber)
    {
        this.EnemyPrefab = prefab;
        this.WaveNumber = waveNumber;
    }
}