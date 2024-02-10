using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : Singleton<WaveManager>
{
    private int waveNumber;
    private bool waveStarted = false;
    private float timePassedSinceWaveStarted = 0f;
    [SerializeField] private Transform startPoint;
    Wave currentWave;
    [SerializeField]
    private List<Wave> level1Waves, level2Waves, level3Waves,
     level4Waves, level5Waves, level6Waves, level7Waves, level8Waves, level9Waves, level10Waves;

    private void Update()
    {
        if (waveStarted)
        {
            timePassedSinceWaveStarted += Time.deltaTime;
            foreach (var spawn in currentWave.SpawnPoints)
            {
                if (!spawn.HasSpawn && timePassedSinceWaveStarted > spawn.SpawnTime)
                    SpawnEmeny(spawn);
            }
        }
    }

    void SpawnEmeny(Spawn spawn)
    {
        GameObject obj = Instantiate(spawn.EnemyPrefab, startPoint.position, Quaternion.identity);
    }
}