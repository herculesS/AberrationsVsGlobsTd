using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnTower : MonoBehaviour
{
    Tilemap arenaMap;

    List<ArenaTile> _spownTiles = new List<ArenaTile>();

    [SerializeField] private GameObject tempTowerPrefab;

    public List<ArenaTile> SpownTiles { get => _spownTiles; set => _spownTiles = value; }

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
