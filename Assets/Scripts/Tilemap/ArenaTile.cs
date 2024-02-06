using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTile
{
    private GameObject _tower;
    private Vector3 _tilePositon;

    public GameObject Tower { get => _tower; set => _tower = value; }
    public Vector3 TilePositon { get => _tilePositon; set => _tilePositon = value; }


    public static bool IsTileSpown(Vector3Int gridPosition, List<ArenaTile> arenaTiles)
    {
        foreach (ArenaTile tile in arenaTiles)
        {
            if (Vector3.Distance(tile.TilePositon, gridPosition) < 0.1f)
            {
                return true;
            }
        }
        return false;
    }


}
