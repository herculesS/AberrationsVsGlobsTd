using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class SpawnTowerManager : MonoBehaviour
{

    public static SpawnTowerManager Instance { get; private set; }

    public EventHandler<ArenaTileClickedArgs> OnArenaTileClicked;
    private bool wasTileClicked = false;
    private Vector3 tileClickedPosition;

    Tilemap arenaMap;

    List<ArenaTile> _spawnedTiles = new List<ArenaTile>();

    [SerializeField] private GameObject tempTowerPrefab;

    public List<ArenaTile> SpownTiles { get => _spawnedTiles; set => _spawnedTiles = value; }
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
        arenaMap = GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        OnArenaTileClicked?.Invoke(this, new ArenaTileClickedArgs());
        //SpawnTile();
    }

    public void HandleTowerSelected(object sender, TowerSelectionArgs args)
    {
         Debug.Log("Tower selected should spawn");
    }

    void SpawnTile()
    {
        Vector3Int PointInCellGrid = arenaMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (!ArenaTile.IsTileSpown(PointInCellGrid, SpownTiles))
        {
            Vector3 clickedCellPosition = arenaMap.GetCellCenterWorld(PointInCellGrid);
            GameObject obj = Instantiate(tempTowerPrefab, clickedCellPosition, Quaternion.identity);
            ArenaTile arenaTile = new ArenaTile();
            arenaTile.Tower = obj;
            arenaTile.TilePositon = PointInCellGrid;
            SpownTiles.Add(arenaTile);
        }
    }
}
