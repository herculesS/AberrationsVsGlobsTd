using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : Singleton<TilemapManager>
{

    [SerializeField] private Tilemap backGround, arena, path;
    public Vector3Int getTotalTileMapsSize()
    {
        return backGround.size;
    }
    public Vector3Int getPathTileMapSize()
    {
        return path.size;
    }

    public Vector3Int GetFirstArenaPath()
    {
        Vector3Int size = arena.size;
        for (int i = arena.origin.y; i < arena.origin.y + arena.cellBounds.size.y; i++)
        {
            Vector3Int gridPosition = new Vector3Int(arena.origin.x, i, 0);
            if (arena.HasTile(gridPosition))
            {
                return gridPosition;
            }
        }
        return Vector3Int.zero;
    }
    public Vector3Int GetLastArenaPath()
    {
        Vector3Int size = arena.size;
        for (int i = arena.origin.y; i < arena.origin.y + arena.cellBounds.size.y; i++)
        {
            Vector3Int gridPosition = new Vector3Int(arena.origin.x + arena.cellBounds.size.x - 1, i, 0);
            if (arena.HasTile(gridPosition))
            {
                return gridPosition;
            }
        }
        return Vector3Int.zero;
    }

    public Vector3 getPathTileCenterPosition(Vector3Int gridPosition)
    {
        return path.GetCellCenterWorld(gridPosition);
    }

    public NodeGrid getPathNodes()
    {
        Vector3Int size = path.size;
        Vector3Int pathOrigin = path.origin;

        int width = size.x;
        int height = size.y;
        NodeGrid grid = new NodeGrid(new NodeGrid.GridPosition(pathOrigin.x, pathOrigin.y), width, height);
        foreach (var pair in grid.All())
        {
            if (!path.HasTile(new Vector3Int(pair.Value.X, pair.Value.Y, 0)))
            {
                grid.getNode(pair.Key).IsWalkable = false;
            }
        }
        return grid;
    }

}
