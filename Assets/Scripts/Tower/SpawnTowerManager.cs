using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class SpawnTowerManager : Singleton<SpawnTowerManager>
{
    public List<TowerSO> listOfTowers;

    public EventHandler<ArenaTileClickedArgs> OnArenaTileClicked;

    private TilemapCollider2D arenaCollider;
    private bool wasTileClicked = false;
    private Vector3Int tileClickedPosition;

    Tilemap arenaMap;

    List<ArenaTile> _spawnedTiles = new List<ArenaTile>();

    [SerializeField] private GameObject tempTowerPrefab;

    public List<ArenaTile> SpownTiles { get => _spawnedTiles; set => _spawnedTiles = value; }

    // Start is called before the first frame update
    void Start()
    {
        arenaMap = GetComponent<Tilemap>();
        arenaCollider = arenaMap.GetComponent<TilemapCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        tileClickedPosition = arenaMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (!ArenaTile.IsTileSpown(tileClickedPosition, SpownTiles))
        {
            wasTileClicked = true;
            arenaCollider.enabled = false;
            OnArenaTileClicked?.Invoke(this, new ArenaTileClickedArgs(listOfTowers));
        }
    }

    public void HandleTowerSelected(object sender, TowerSelectionArgs args)
    {
        wasTileClicked = false;
        arenaCollider.enabled = true;

        SpawnTile(args.TowerPrefab);
    }

    void SpawnTile(GameObject towerPrefab)
    {
        Vector3 clickedCellPosition = arenaMap.GetCellCenterWorld(tileClickedPosition);
        GameObject obj = Instantiate(towerPrefab, clickedCellPosition, Quaternion.identity);
        ArenaTile arenaTile = new ArenaTile();
        arenaTile.Tower = obj;
        arenaTile.TilePositon = tileClickedPosition;
        SpownTiles.Add(arenaTile);
    }
}
