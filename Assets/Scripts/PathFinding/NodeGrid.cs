using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeGrid
{
    int width;
    int height;
    Dictionary<GridPosition, Node> grid = new Dictionary<GridPosition, Node>();

    public NodeGrid(GridPosition start, int width, int height)
    {
        this.width = width;
        this.height = height;
        for (int y = start.Y; y < start.Y + height; y++)
        {
            for (int x = start.X; x < start.X + width; x++)
            {
                GridPosition pos = new GridPosition(x, y);
                grid.Add(pos, new Node(x, y));
            }
        }
    }

    public bool hasNode(GridPosition position)
    {
        return grid.ContainsKey(position);
    }

    public Node getNode(GridPosition position)
    {
        if (!grid.ContainsKey(position))
        {
            return null;

        }
        return grid[position];
    }

    public Dictionary<GridPosition, Node> All()
    {
        return grid;
    }

    public struct GridPosition
    {
        int x, y;
        public GridPosition(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return !(a == b);
        }
    }
}
