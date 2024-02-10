using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using System;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<Vector3> pathInWorldPositions = new List<Vector3>();
    [SerializeField] private float enemyPerSecond = 0.5f;
    private float timeSinceLastEnemy = 0f;

    private List<GameObject> enemiesSpawned = new List<GameObject>();
    private List<Node> path;
    private PathFinder pathFinder;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new PathFinder(TilemapManager.Instance.getPathNodes());
        path = pathFinder.findPath(new Node(-11, -1), new Node(10, -2));
        foreach (Node node in path)
        {
            Vector3 tilePosition = TilemapManager.Instance.getPathTileCenterPosition(new Vector3Int(node.X, node.Y, 0));
            pathInWorldPositions.Add(tilePosition);
        }
        WaveManager.Instance.onSpawnPoint += SpawnEnemy;
        //StartCoroutine(IncreaseEnemyPerSecond(2f));
    }
    IEnumerator IncreaseEnemyPerSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        enemyPerSecond += 0.1f;
        StartCoroutine(IncreaseEnemyPerSecond(seconds));
    }
    void FixedUpdate()
    {
        if (timeSinceLastEnemy > 1f / enemyPerSecond)
        {
            timeSinceLastEnemy = 0f;
            //SpawnEnemy();
        }
        else
        {
            timeSinceLastEnemy += Time.fixedDeltaTime;
        }
    }
    void SpawnEnemy(object sender, SpawnPointEventArgs args)
    {
        GameObject enemyPrefab = args.EnemyPrefab;
        GameObject obj = Instantiate(enemyPrefab, pathInWorldPositions[0], Quaternion.identity);
        enemiesSpawned.Add(obj);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.SetPath(pathInWorldPositions);
        enemy.Health = (int)(enemy.Health * Mathf.Exp(MainGameConfig.WaveGrowthFactor));
        enemy.onKilled += DestroyEnemy;
        enemy.onKilled += CurrencyManager.Instance.HandleEnemyKilled;
    }

    public void DestroyEnemy(object sender, EnemyKilledEventArgs args)
    {
        int enemyIndex = enemiesSpawned.IndexOf(args.KilledEnemy);
        if (enemyIndex > -1)
        {
            enemiesSpawned.RemoveAt(enemyIndex);
            Destroy(args.KilledEnemy);
        }
        else
        {
            Debug.Log("Enemy Not Found");
        }

    }
    public GameObject GetClosestEnemyPosition(Vector3 positionToCompare, float range = -1f)
    {
        Vector3 closestPosition = new Vector3(10000f, 10000f, 0);
        Vector3 maxRangePosition = new Vector3(10000f, 10000f, 0);
        GameObject closestObj = null;
        foreach (GameObject obj in enemiesSpawned)
        {
            float distanceToclosest = Vector3.Distance(positionToCompare, closestPosition);
            float distanceToEnemy = Vector3.Distance(positionToCompare, obj.transform.position);
            if (distanceToEnemy < distanceToclosest)
            {
                closestObj = obj;
                closestPosition = obj.transform.position;
            }
        }

        if (range > -1f)
        {
            if (Vector3.Distance(closestPosition, positionToCompare) <= range)
            {
                return closestObj;
            }
            else
            {
                return null;
            }
        }
        else
        {
            if (closestPosition == maxRangePosition)
            {
                return closestObj;
            }
            else
            {
                return null;
            }
        }

    }
}
