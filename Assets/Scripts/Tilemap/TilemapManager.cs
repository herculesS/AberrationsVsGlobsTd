using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : Singleton<TilemapManager>
{

    [SerializeField] private Tilemap backGround, arena, path;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
        Debug.Log("Arena Origin" + arena.origin.x + "/" + arena.origin.y);
        Debug.Log("Arena size" + arena.size.x + "/" + arena.size.y);
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

    public Node[,] getPathNodes()
    {
        Vector3Int size = path.size;

        int width = size.x;
        int height = size.y;
        Node[,] nodes = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                nodes[x, y] = new Node(x, y);
                /* if (!path.HasTile(new Vector3Int(x - width / 2, y - height, 0)))
                {
                    nodes[x, y].IsWalkable = false;
                } */
            }
        }
        return nodes;
    }

}
