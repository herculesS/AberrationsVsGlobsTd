using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    private List<Vector3> _locations = new List<Vector3>();
    [SerializeField] private Tilemap pathMap;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemyPerSecond = 0.5f;
    private float timeSinceLastEnemy = 0f;

    private List<GameObject> enemiesSpawned = new List<GameObject>();
    public static EnemyManager Instance { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeLocations();
    }

    private void InitializeLocations()
    {
        Stack<Vector3> orderingStack = new Stack<Vector3>();
        BoundsInt bounds = pathMap.cellBounds;
        int startx = pathMap.origin.x;
        int startY = pathMap.origin.y;
        for (int x = startx; x < startx + bounds.size.x; x++)
        {
            for (int y = startY; y < startY + bounds.size.y; y++)
            {
                Vector3Int gridCoordinate = new Vector3Int(x, y, 0);

                if (pathMap.HasTile(gridCoordinate))
                {

                    Vector3 tilePosition = pathMap.GetCellCenterWorld(gridCoordinate);
                    if (_locations.Count == 0)
                    {
                        _locations.Add(tilePosition);
                        continue;
                    }
                    Vector3 LastPositionAdded = _locations[_locations.Count - 1];
                    float absValue = Mathf.Abs(LastPositionAdded.x - tilePosition.x) +
                        Mathf.Abs(LastPositionAdded.y - tilePosition.y);
                    if (absValue > 1.01f)
                    {
                        orderingStack.Push(tilePosition);
                    }
                    else
                    {
                        _locations.Add(tilePosition);
                        while (orderingStack.Count > 0)
                        {
                            _locations.Add(orderingStack.Pop());
                        }
                    }

                }

            }
        }
    }
    private void FixedUpdate()
    {
        if (timeSinceLastEnemy > 1f / enemyPerSecond)
        {
            timeSinceLastEnemy = 0f;
            SpawnEnemy();
        }
        else
        {
            timeSinceLastEnemy += Time.fixedDeltaTime;
        }
    }
    void SpawnEnemy()
    {
        GameObject obj = Instantiate(enemyPrefab, _locations[0], Quaternion.identity);
        enemiesSpawned.Add(obj);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.SetPath(_locations);
    }

    public void DestroyEnemy(GameObject enemy)
    {
        int enemyIndex = enemiesSpawned.IndexOf(enemy);
        if (enemyIndex > -1)
        {
            enemiesSpawned.RemoveAt(enemyIndex);
            Destroy(enemy);
        }
        else
        {
            Debug.Log("Enemy Not Found");
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
