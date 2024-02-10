using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class WaveManager : Singleton<WaveManager>
{
    private int waveNumber = 0;
    private bool waveStarted = false;
    private float timePassedSinceWaveStarted = 0f;
    public EventHandler<SpawnPointEventArgs> onSpawnPoint;
    [SerializeField] private Transform startPoint;
    Wave currentWave;
    [SerializeField]
    private List<Wave> level1Waves, level2Waves, level3Waves,
     level4Waves, level5Waves, level6Waves, level7Waves, level8Waves, level9Waves, level10Waves;

    private void Start()
    {
        currentWave = level1Waves[0];
        waveStarted = true;
    }
    private void Update()
    {
        if (waveStarted)
        {
            timePassedSinceWaveStarted += Time.deltaTime;
            foreach (var spawn in currentWave.SpawnPoints)
            {
                if (!spawn.HasSpawn && timePassedSinceWaveStarted > spawn.SpawnTime)
                {
                    spawn.HasSpawn = true;
                    onSpawnPoint?.Invoke(this, new SpawnPointEventArgs(spawn.EnemyPrefab, waveNumber));
                }

            }
            if (currentWave.WaveLength < timePassedSinceWaveStarted)
            {
                waveStarted = false;
            }
        }
    }


}