using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathFinderTest : MonoBehaviour
{
    Node[,] nodes;
    PathFinder finder;
    private void Start()
    {
        nodes = TilemapManager.Instance.getPathNodes();
        Vector3Int size = TilemapManager.Instance.getPathTileMapSize();
        int width = size.x;
        int height = size.y;
        finder = new PathFinder(nodes, width, height);
        Vector3Int startPoint = TilemapManager.Instance.GetFirstArenaPath();
        Vector3Int endPoint = TilemapManager.Instance.GetLastArenaPath();


        List<Node> path = finder.findPath(new Node(0, 1), new Node(21, 0));
        if (path != null)
        {
            foreach (Node item in path)
            {
                Debug.Log("PathNode: " + item.X + " / " + item.Y);
            }
        }
        else
        {
            Debug.Log("Size of path Found");
        }



    }
}