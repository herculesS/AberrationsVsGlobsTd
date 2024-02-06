using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTile
{
    private GameObject tower;
    private Vector3 tilePosition;

    public GameObject Tower { get => tower; set => tower = value; }
    public Vector3 TilePosition { get => tilePosition; set => tilePosition = value; }


    public static bool IsTileSpawned(Vector3Int gridPosition, List<ArenaTile> arenaTiles)
    {
        foreach (ArenaTile tile in arenaTiles)
        {
            if (Vector3.Distance(tile.TilePosition, gridPosition) < 0.1f)
            {
                return true;
            }
        }
        return false;
    }


}
