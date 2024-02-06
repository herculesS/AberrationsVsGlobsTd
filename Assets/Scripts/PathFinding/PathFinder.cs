using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    List<Node> openList = new List<Node>();
    List<Node> closedList = new List<Node>();
    Node[,] nodes;
    int width, height;

    public PathFinder(Node[,] nodes, int width, int height)
    {
        this.nodes = nodes;
        this.width = width;
        this.height = height;
    }
    public List<Node> findPath(Node startPoint, Node endPoint)
    {
        startPoint.G = 0;
        startPoint.H = CalculateDistance(startPoint, endPoint);
        openList.Add(startPoint);
        while (openList.Count > 0)
        {
            Node currentNode = findLeastF(openList);
            Debug.Log("CurrentNode" + currentNode.X + "/" + currentNode.Y);
            if (currentNode == endPoint)
            {
                return CalculatePath(currentNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            foreach (Node nod in GetNeighbors(currentNode))
            {
                if (closedList.Contains(nod)) continue;
                int tantativeGCost = currentNode.G + CalculateDistance(currentNode, nod);
                if (tantativeGCost < nod.G)
                {
                    nod.Parent = currentNode;
                    nod.G = tantativeGCost;
                    nod.H = CalculateDistance(nod, endPoint);
                }
                if (!openList.Contains(nod))
                {
                    openList.Add(nod);
                }
            }
        }

        //No path found
        return null;
    }

    List<Node> GetNeighbors(Node currentNode)
    {
        List<Node> neighbors = new List<Node>();
        if (currentNode.X - 1 >= 0)
        {
            if (nodes[currentNode.X - 1, currentNode.Y].IsWalkable)
                neighbors.Add(nodes[currentNode.X - 1, currentNode.Y]);
            if (currentNode.Y - 1 >= 0)
                if (nodes[currentNode.X - 1, currentNode.Y - 1].IsWalkable)
                    neighbors.Add(nodes[currentNode.X - 1, currentNode.Y - 1]);
            if (currentNode.Y + 1 < height)
                if (nodes[currentNode.X - 1, currentNode.Y + 1].IsWalkable)
                    neighbors.Add(nodes[currentNode.X - 1, currentNode.Y + 1]);
        }
        if (currentNode.X + 1 < width)
        {
            if (nodes[currentNode.X + 1, currentNode.Y].IsWalkable)
                neighbors.Add(nodes[currentNode.X + 1, currentNode.Y]);
            if (currentNode.Y - 1 >= 0)
                if (nodes[currentNode.X + 1, currentNode.Y - 1].IsWalkable)
                    neighbors.Add(nodes[currentNode.X + 1, currentNode.Y - 1]);
            if (currentNode.Y + 1 < height)
                if (nodes[currentNode.X + 1, currentNode.Y + 1].IsWalkable)
                    neighbors.Add(nodes[currentNode.X + 1, currentNode.Y + 1]);
        }
        if (currentNode.Y - 1 >= 0)
            if (nodes[currentNode.X, currentNode.Y - 1].IsWalkable)
                neighbors.Add(nodes[currentNode.X, currentNode.Y - 1]);
        if (currentNode.Y + 1 < height)
            if (nodes[currentNode.X, currentNode.Y + 1].IsWalkable)
                neighbors.Add(nodes[currentNode.X, currentNode.Y + 1]);

        return neighbors;
    }

    Node findLeastF(List<Node> list)
    {
        Node lowest = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].F < lowest.F)
            {
                lowest = list[i];
            }
        }

        return lowest;
    }

    int CalculateDistance(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return 14 * Mathf.Min(xDistance, yDistance) + 10 * remaining;
    }
    List<Node> CalculatePath(Node endPoint)
    {
        Debug.Log("Path ended");
        List<Node> path = new List<Node>();
        path.Add(endPoint);
        Node currentNode = endPoint;
        while (currentNode.Parent != null)
        {
            path.Add(currentNode.Parent);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }
}
