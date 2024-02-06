using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathFinderTest : MonoBehaviour
{
    NodeGrid grid;
    PathFinder finder;
    private void Start()
    {
        grid = TilemapManager.Instance.getPathNodes();
        finder = new PathFinder(grid);
        List<Node> path = finder.findPath(new Node(-11, -1), new Node(10, -2));  

    }
}