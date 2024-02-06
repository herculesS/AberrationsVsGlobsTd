using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node
{
    Node parent;
    int x, y;
    int g, h;
    bool isWalkable = true;
    public int G { get => g; set => g = value; }
    public int H { get => h; set => h = value; }
    public int F { get { return G + H; } }

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public Node Parent { get => parent; set => parent = value; }
    public bool IsWalkable { get => isWalkable; set => isWalkable = value; }

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        g = Int32.MaxValue;
        parent = null;
    }
    public static bool operator ==(Node a, Node b)
    {
        if (a is null)
        {
            return b is null;
        }
        if (b is null) return false;
        return a.X == b.X && a.Y == b.Y;
    }
    public static bool operator !=(Node a, Node b)
    {
        return !(a == b);
    }
}
