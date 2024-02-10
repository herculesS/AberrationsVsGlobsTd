using UnityEngine;
[CreateAssetMenu(fileName = "Spawn", menuName = "ScriptableObject/Spawn", order = 1)]
public class Spawn : ScriptableObject
{
    public float SpawnTime ;
    public GameObject EnemyPrefab ;

    public bool HasSpawn = false;
}